namespace WiseBuddy.Api.Dto;

public record SuitabilityCreateDto
{
    public int UsuarioId { get; set; }
    public decimal RendaMensal { get; set; }
    public int IdadeInvestidor { get; set; }
    public int TempoInvestimento { get; set; }
    public string ObjetivoInvestimento { get; set; } = string.Empty;
    public IEnumerable<RespostaSuitabilityDto> Respostas { get; set; } = new List<RespostaSuitabilityDto>();
}
