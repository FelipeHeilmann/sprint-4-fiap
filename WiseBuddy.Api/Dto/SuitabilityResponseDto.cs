namespace WiseBuddy.Api.Dto;

public class SuitabilityResponseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string PerfilInvestidor { get; set; } = string.Empty;
    public int PontuacaoTotal { get; set; }
    public decimal RendaMensal { get; set; }
    public string ObjetivoInvestimento { get; set; } = string.Empty;
    public DateTime DataTeste { get; set; }
    public IEnumerable<RespostaCalculadaDto> Respostas { get; set; } = new List<RespostaCalculadaDto>(); 
    public string DescricaoPerfil { get; set; } = string.Empty;
}
