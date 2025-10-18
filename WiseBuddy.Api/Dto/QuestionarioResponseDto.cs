namespace WiseBuddy.Api.Dto;

public record QuestionarioResponseDto
{
    public int Id { get; set; }
    public string Pergunta { get; set; } = string.Empty;
    public IEnumerable<string> Opcoes { get; set; } = new List<string>();
}
