using System.ComponentModel.DataAnnotations;

namespace WiseBuddy.Api.Models;

public class SuitabilityResposta
{
    public int Id { get; set; }
    public int SuitabilityId { get; set; }

    public int PerguntaId { get; set; }

    [Required, MaxLength(500)]
    public string TextoPergunta { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string RespostaSelecionada { get; set; } = string.Empty;

    public int PontuacaoObtida { get; set; }

    public DateTime DataResposta { get; set; } = DateTime.UtcNow;

    // Navegação
    public Suitability Suitability { get; set; } = null!;
}
