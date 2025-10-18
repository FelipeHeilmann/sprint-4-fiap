namespace WiseBuddy.Api.Dto;

public record RecomendacaoExportDto
{
    public string TipoAtivo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal PercentualSugerido { get; set; }
    public decimal RentabilidadeEsperada { get; set; }
    public string NivelRisco { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
