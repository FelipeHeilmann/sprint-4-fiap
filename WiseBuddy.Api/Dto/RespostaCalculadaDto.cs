namespace WiseBuddy.Api.Dto;

public record RespostaCalculadaDto
{
    public int PerguntaId { get; set; }
    public string Pergunta { get; set; } = string.Empty;
    public string RespostaSelecionada { get; set; } = string.Empty;
    public int PontuacaoObtida { get; set; }
}
