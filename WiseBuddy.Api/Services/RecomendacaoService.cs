using System.Text;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Models;
using WiseBuddy.Api.Repositories;

namespace WiseBuddy.Api.Services;

public class RecomendacaoService
{
    private readonly IRecomendacaoRepository _recomendacaoRepository;
    private readonly ISuitabilityRepository _suitabilityRepository;

    public RecomendacaoService
    (
        IRecomendacaoRepository recomendacaoRepository,
        ISuitabilityRepository suitabilityRepository
    )
    {
        _recomendacaoRepository = recomendacaoRepository;
        _suitabilityRepository = suitabilityRepository;
    }

    public async Task<IEnumerable<RecomendacaoResponseDto>> GenerateRecommendationsAsync(int usuarioId)
    {
        var latestSuitability = await _suitabilityRepository.GetLatestByUsuarioAsync(usuarioId);
        if (latestSuitability == null)
        {
            throw new InvalidOperationException("Usuário precisa completar o teste de suitability primeiro");
        }

        await _recomendacaoRepository.DeactivateByUsuarioAsync(usuarioId);

        var recomendacoes = GenerateByProfile(usuarioId, latestSuitability);

        foreach (var rec in recomendacoes)
        {
            await _recomendacaoRepository.AddAsync(rec);
        }

        return recomendacoes.Select(r => new RecomendacaoResponseDto
        {
            Id = r.Id,
            TipoAtivo = r.TipoAtivo,
            Descricao = r.Descricao,
            PercentualSugerido = r.PercentualSugerido,
            RentabilidadeEsperada = r.RentabilidadeEsperada,
            NivelRisco = r.NivelRisco,
            Ativa = r.Ativa,
            DataCriacao = r.DataCriacao,
            UsuarioId = r.UsuarioId
        });
    }

    public async Task<IEnumerable<RecomendacaoResponseDto>> GetByUsuarioAsync(int usuarioId)
    {
        var recomendacoes = await _recomendacaoRepository.GetActiveByUsuarioAsync(usuarioId);
        return recomendacoes.Select(r => new RecomendacaoResponseDto
        {
            Id = r.Id,
            TipoAtivo = r.TipoAtivo,
            Descricao = r.Descricao,
            PercentualSugerido = r.PercentualSugerido,
            RentabilidadeEsperada = r.RentabilidadeEsperada,
            NivelRisco = r.NivelRisco,
            Ativa = r.Ativa,
            DataCriacao = r.DataCriacao,
            UsuarioId = r.UsuarioId
        });
    }


    public async Task<byte[]> ExportRecommendationsToTxtAsync(int usuarioId)
    {
        var recomendacoes = await GetByUsuarioAsync(usuarioId);
        var sb = new StringBuilder();

        sb.AppendLine("=== RELATÓRIO DE RECOMENDAÇÕES DE INVESTIMENTO ===");
        sb.AppendLine($"Usuário ID: {usuarioId}");
        sb.AppendLine($"Data: {DateTime.Now:dd/MM/yyyy HH:mm}");
        sb.AppendLine();
        sb.AppendLine("RECOMENDAÇÕES:");
        sb.AppendLine();

        foreach (var rec in recomendacoes)
        {
            sb.AppendLine($"• {rec.TipoAtivo} ({rec.PercentualSugerido}%)");
            sb.AppendLine($"  Risco: {rec.NivelRisco}");
            sb.AppendLine($"  Rentabilidade Esperada: {rec.RentabilidadeEsperada}% a.a.");
            sb.AppendLine($"  Descrição: {rec.Descricao}");
            sb.AppendLine();
        }

        sb.AppendLine("=== FIM DO RELATÓRIO ===");

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private IEnumerable<Recomendacao> GenerateByProfile(int usuarioId, Suitability suitability)
    {
        return suitability.PerfilInvestidor switch
        {
            "Conservador" => GetConservativeRecommendations(usuarioId),
            "Moderado" => GetModerateRecommendations(usuarioId),
            "Agressivo" => GetAggressiveRecommendations(usuarioId),
            _ => GetConservativeRecommendations(usuarioId)
        };
    }

    private IEnumerable<Recomendacao> GetConservativeRecommendations(int usuarioId)
    {
        return new List<Recomendacao>
            {
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "Tesouro Selic",
                    Descricao = "Título público pós-fixado, acompanha a taxa Selic. Ideal para reserva de emergência.",
                    PercentualSugerido = 40,
                    RentabilidadeEsperada = 10.5m,
                    NivelRisco = "Muito Baixo"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "CDB",
                    Descricao = "Certificado de Depósito Bancário com garantia do FGC até R$ 250.000.",
                    PercentualSugerido = 35,
                    RentabilidadeEsperada = 11.0m,
                    NivelRisco = "Baixo"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "LCI/LCA",
                    Descricao = "Letras de Crédito Imobiliário/Agronegócio. Isentas de IR para pessoa física.",
                    PercentualSugerido = 25,
                    RentabilidadeEsperada = 9.8m,
                    NivelRisco = "Baixo"
                }
            };
    }

    private IEnumerable<Recomendacao> GetModerateRecommendations(int usuarioId)
    {
        return new List<Recomendacao>
        {
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "Tesouro IPCA+",
                    Descricao = "Título público indexado à inflação, protege o poder de compra.",
                    PercentualSugerido = 25,
                    RentabilidadeEsperada = 12.5m,
                    NivelRisco = "Médio"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "Fundos Multimercado",
                    Descricao = "Fundos com estratégias diversificadas, gestão ativa profissional.",
                    PercentualSugerido = 30,
                    RentabilidadeEsperada = 14.0m,
                    NivelRisco = "Médio"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "Ações Blue Chips",
                    Descricao = "Ações de empresas consolidadas com boa governança e dividendos.",
                    PercentualSugerido = 25,
                    RentabilidadeEsperada = 16.0m,
                    NivelRisco = "Médio-Alto"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "CDB",
                    Descricao = "Complemento em renda fixa para equilibrar a carteira.",
                    PercentualSugerido = 20,
                    RentabilidadeEsperada = 11.0m,
                    NivelRisco = "Baixo"
                }
            };
    }

    private IEnumerable<Recomendacao> GetAggressiveRecommendations(int usuarioId)
    {
        return new List<Recomendacao>
            {
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "Ações Growth",
                    Descricao = "Ações de empresas em crescimento com alto potencial de valorização.",
                    PercentualSugerido = 40,
                    RentabilidadeEsperada = 20.0m,
                    NivelRisco = "Alto"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "ETFs Internacionais",
                    Descricao = "Fundos que replicam índices internacionais, diversificação global.",
                    PercentualSugerido = 25,
                    RentabilidadeEsperada = 18.0m,
                    NivelRisco = "Alto"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "FIIs",
                    Descricao = "Fundos de Investimento Imobiliário, renda passiva através de aluguéis.",
                    PercentualSugerido = 20,
                    RentabilidadeEsperada = 15.0m,
                    NivelRisco = "Médio-Alto"
                },
                new Recomendacao
                {
                    UsuarioId = usuarioId,
                    TipoAtivo = "Tesouro IPCA+",
                    Descricao = "Proteção contra inflação, âncora da carteira.",
                    PercentualSugerido = 15,
                    RentabilidadeEsperada = 12.5m,
                    NivelRisco = "Médio"
                }
            };
    }
}
