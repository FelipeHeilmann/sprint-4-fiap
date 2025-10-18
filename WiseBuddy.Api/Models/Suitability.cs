using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WiseBuddy.Api.Models;

public class Suitability
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }

    [Required, MaxLength(50)]
    public string PerfilInvestidor { get; set; } = string.Empty;

    public int PontuacaoTotal { get; set; }
    public decimal RendaMensal { get; set; }
    public int IdadeInvestidor { get; set; }
    public int TempoInvestimento { get; set; }

    [MaxLength(100)]
    public string ObjetivoInvestimento { get; set; } = string.Empty;

    public DateTime DataTeste { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; } = null!;
    public Collection<SuitabilityResposta> Respostas { get; set; } = new();

}

