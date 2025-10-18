using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WiseBuddy.Api.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(15)]
    public string? Telefone { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public bool Ativo { get; set; } = true;

    public Collection<Suitability> Suitabilities { get; set; } = new();
    public Collection<Recomendacao> Recomendacoes { get; set; } = new();
}
