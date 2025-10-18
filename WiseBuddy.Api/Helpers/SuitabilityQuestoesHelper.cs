using WiseBuddy.Api.Models;

namespace WiseBuddy.Api.Helpers;

public class SuitabilityQuestoesHelper
{
    public static IEnumerable<QuestionarioSuitability> ObterPerguntas()
    {
        return new List<QuestionarioSuitability>
            {
                new QuestionarioSuitability
                {
                    Id = 1,
                    Pergunta = "Qual é a sua experiência com investimentos?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Nunca investi", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "Pouca experiência (poupança/CDB)", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "Experiência moderada (fundos/tesouro)", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Experiência avançada (ações/derivativos)", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 2,
                    Pergunta = "Como você reagiria se seus investimentos perdessem 20% do valor em um mês?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Venderia tudo imediatamente", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "Ficaria muito preocupado e consideraria vender", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "Manteria a posição e aguardaria a recuperação", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Aproveitaria para comprar mais com desconto", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 3,
                    Pergunta = "Qual é o seu principal objetivo com os investimentos?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Preservar capital", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "Renda regular", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "Crescimento moderado", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Máximo crescimento possível", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 4,
                    Pergunta = "Por quanto tempo pretende manter o dinheiro investido?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Menos de 1 ano", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "1 a 3 anos", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "3 a 5 anos", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Mais de 5 anos", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 5,
                    Pergunta = "Que percentual da sua renda você pode investir sem comprometer seu padrão de vida?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Até 10%", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "10% a 25%", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "25% a 50%", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Mais de 50%", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 6,
                    Pergunta = "Você possui reserva de emergência (6 meses de gastos)?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Não tenho reserva", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "Tenho menos de 3 meses", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "Tenho entre 3-6 meses", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Tenho mais de 6 meses", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 7,
                    Pergunta = "Como você descreveria seu conhecimento sobre o mercado financeiro?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Muito básico", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "Básico", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "Intermediário", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Avançado", Pontuacao = 4 }
                    }
                },
                new QuestionarioSuitability
                {
                    Id = 8,
                    Pergunta = "Qual seria sua reação se um investimento desse retorno negativo no primeiro ano?",
                    Opcoes = new List<OpcaoResposta>
                    {
                        new OpcaoResposta { Valor = "Mudaria imediatamente para algo mais seguro", Pontuacao = 1 },
                        new OpcaoResposta { Valor = "Ficaria preocupado mas manteria", Pontuacao = 2 },
                        new OpcaoResposta { Valor = "Entenderia que faz parte do processo", Pontuacao = 3 },
                        new OpcaoResposta { Valor = "Veria como oportunidade de aprendizado", Pontuacao = 4 }
                    }
                }
            };
    }
}
