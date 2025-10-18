namespace WiseBuddy.Api.Models;

public class QuestionarioSuitability
{
    public int Id { get; set; }
    public string Pergunta { get; set; } = string.Empty;
    public IEnumerable<OpcaoResposta> Opcoes { get; set; } = new List<OpcaoResposta>();
}
