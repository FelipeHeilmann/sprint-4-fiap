namespace WiseBuddy.Api.Dto;

public record CotacaoResponse
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public string? Simbolo { get; set; }
    public double Preco { get; set; }
    public double ValorMercado { get; set; }
    public double Variacao24h { get; set; }
    public double Volume { get; set; }
}
