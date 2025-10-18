using System.ComponentModel.DataAnnotations;

namespace WiseBuddy.Api.Models;

public class Recomendacao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }

    [Required, MaxLength(100)]
    public string TipoAtivo { get; set; } = string.Empty;

    [Required]
    public string Descricao { get; set; } = string.Empty;

    public decimal PercentualSugerido { get; set; }
    public decimal RentabilidadeEsperada { get; set; }

    [MaxLength(50)]
    public string NivelRisco { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public bool Ativa { get; set; } = true;

    // Navegação
    public Usuario Usuario { get; set; } = null!;
}
