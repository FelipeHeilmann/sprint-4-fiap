namespace WiseBuddy.Api.Dto;

public record RespostaSuitabilityDto
{
    public int PerguntaId { get; set; }
    public string RespostaSelecionada { get; set; } = string.Empty;
}