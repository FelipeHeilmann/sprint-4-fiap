using AutoMapper;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Helpers;
using WiseBuddy.Api.Models;
using WiseBuddy.Api.Repositories;

namespace WiseBuddy.Api.Services;

public class SuitabilityService
{
    private readonly ISuitabilityRepository _repository;

    public SuitabilityService(ISuitabilityRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<QuestionarioResponseDto> ObterQuestionario()
    {
        var perguntas = SuitabilityQuestoesHelper.ObterPerguntas();
        return perguntas.Select(p => new QuestionarioResponseDto
        {
            Id = p.Id,
            Pergunta = p.Pergunta,
            Opcoes = p.Opcoes.Select(o => o.Valor).ToList()
        });
    }

    public async Task<SuitabilityResponseDto> CreateAsync(SuitabilityCreateDto dto)
    {
        var perguntas = SuitabilityQuestoesHelper.ObterPerguntas();
        var perguntasIds = perguntas.Select(p => p.Id).ToList();
        var respostasIds = dto.Respostas.Select(r => r.PerguntaId).ToList();

        if (!perguntasIds.All(id => respostasIds.Contains(id)))
        {
            throw new InvalidOperationException("Todas as perguntas devem ser respondidas");
        }

        var respostasCalculadas = new List<RespostaCalculadaDto>();
        var pontuacaoTotal = 0;

        foreach (var resposta in dto.Respostas)
        {
            var pergunta = perguntas.FirstOrDefault(p => p.Id == resposta.PerguntaId);
            if (pergunta == null)
            {
                throw new InvalidOperationException($"Pergunta {resposta.PerguntaId} não existe");
            }

            var opcaoEscolhida = pergunta.Opcoes.FirstOrDefault(o => o.Valor == resposta.RespostaSelecionada);
            if (opcaoEscolhida == null)
            {
                throw new InvalidOperationException($"Resposta '{resposta.RespostaSelecionada}' não é válida para a pergunta {resposta.PerguntaId}");
            }

            pontuacaoTotal += opcaoEscolhida.Pontuacao;

            respostasCalculadas.Add(new RespostaCalculadaDto
            {
                PerguntaId = resposta.PerguntaId,
                Pergunta = pergunta.Pergunta,
                RespostaSelecionada = resposta.RespostaSelecionada,
                PontuacaoObtida = opcaoEscolhida.Pontuacao
            });
        }

        var perfil = CalcularPerfilInvestidor(dto, pontuacaoTotal);

        var suitability = new Suitability
        {
            UsuarioId = dto.UsuarioId,
            PerfilInvestidor = perfil,
            PontuacaoTotal = pontuacaoTotal,
            RendaMensal = dto.RendaMensal,
            IdadeInvestidor = dto.IdadeInvestidor,
            TempoInvestimento = dto.TempoInvestimento,
            ObjetivoInvestimento = dto.ObjetivoInvestimento,
            DataTeste = DateTime.UtcNow
        };

        var createdSuitability = await _repository.AddAsync(suitability);

        foreach (var respostaCalc in respostasCalculadas)
        {
            var suitabilityResposta = new SuitabilityResposta
            {
                SuitabilityId = createdSuitability.Id,
                PerguntaId = respostaCalc.PerguntaId,
                TextoPergunta = respostaCalc.Pergunta,
                RespostaSelecionada = respostaCalc.RespostaSelecionada,
                PontuacaoObtida = respostaCalc.PontuacaoObtida,
                DataResposta = DateTime.UtcNow
            };

            await _repository.AddRespostaAsync(suitabilityResposta);
        }

        return new SuitabilityResponseDto
        {
            Id = createdSuitability.Id,
            UsuarioId = createdSuitability.UsuarioId,
            PerfilInvestidor = createdSuitability.PerfilInvestidor,
            PontuacaoTotal = createdSuitability.PontuacaoTotal,
            RendaMensal = createdSuitability.RendaMensal,
            ObjetivoInvestimento = createdSuitability.ObjetivoInvestimento,
            DataTeste = createdSuitability.DataTeste,
            Respostas = respostasCalculadas,
            DescricaoPerfil = ObterDescricaoPerfil(perfil)
        };
    }

    public async Task<SuitabilityResponseDto?> GetLatestByUsuarioAsync(int usuarioId)
    {
        var suitability = await _repository.GetLatestByUsuarioAsync(usuarioId);
        if (suitability == null) return null;


        return new SuitabilityResponseDto
        {
            Id = suitability.Id,
            UsuarioId = suitability.UsuarioId,
            PerfilInvestidor = suitability.PerfilInvestidor,
            PontuacaoTotal = suitability.PontuacaoTotal,
            RendaMensal = suitability.RendaMensal,
            ObjetivoInvestimento = suitability.ObjetivoInvestimento,
            DataTeste = suitability.DataTeste,
            Respostas = suitability.Respostas.Select(r => new RespostaCalculadaDto
            {
                PerguntaId = r.PerguntaId,
                Pergunta = r.TextoPergunta,
                RespostaSelecionada = r.RespostaSelecionada,
                PontuacaoObtida = r.PontuacaoObtida
            }),
            DescricaoPerfil = ObterDescricaoPerfil(suitability.PerfilInvestidor)
        };
    }

    public async Task<IEnumerable<SuitabilityResponseDto>> GetHistoryByUsuarioAsync(int usuarioId)
    {
        var history = await _repository.GetHistoryByUsuarioAsync(usuarioId);

        return history.Select(suitability => new SuitabilityResponseDto
        {
            Id = suitability.Id,
            UsuarioId = suitability.UsuarioId,
            PerfilInvestidor = suitability.PerfilInvestidor,
            PontuacaoTotal = suitability.PontuacaoTotal,
            RendaMensal = suitability.RendaMensal,
            ObjetivoInvestimento = suitability.ObjetivoInvestimento,
            DataTeste = suitability.DataTeste,
            Respostas = suitability.Respostas.Select(r => new RespostaCalculadaDto
            {
                PerguntaId = r.PerguntaId,
                Pergunta = r.TextoPergunta,
                RespostaSelecionada = r.RespostaSelecionada,
                PontuacaoObtida = r.PontuacaoObtida
            }),
            DescricaoPerfil = ObterDescricaoPerfil(suitability.PerfilInvestidor)
        });
    }

    public string CalcularPerfilInvestidor(SuitabilityCreateDto dto, int pontuacaoBase)
    {
        var pontuacaoAjustada = pontuacaoBase;

        if (dto.IdadeInvestidor <= 30) pontuacaoAjustada += 2;
        else if (dto.IdadeInvestidor >= 55) pontuacaoAjustada -= 2;

        if (dto.TempoInvestimento >= 10) pontuacaoAjustada += 3;
        else if (dto.TempoInvestimento <= 2) pontuacaoAjustada -= 2;

        if (dto.RendaMensal >= 20000) pontuacaoAjustada += 1;
        else if (dto.RendaMensal <= 3000) pontuacaoAjustada -= 1;

        return pontuacaoAjustada switch
        {
            <= 18 => "Conservador",
            <= 26 => "Moderado",
            _ => "Agressivo"
        };
    }

    private string ObterDescricaoPerfil(string perfil)
    {
        return perfil switch
        {
            "Conservador" => "Busca preservação do capital com baixo risco. Prefere investimentos em renda fixa com garantia.",
            "Moderado" => "Busca equilíbrio entre segurança e rentabilidade. Aceita algum risco para obter melhores retornos.",
            "Agressivo" => "Busca máxima rentabilidade e aceita altos riscos. Tem conhecimento e experiência no mercado.",
            _ => "Perfil não identificado"
        };
    }
}