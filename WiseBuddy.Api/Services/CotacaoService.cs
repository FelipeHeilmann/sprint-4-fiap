using System.Collections;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Gateway;

namespace WiseBuddy.Api.Services;

public class CotacaoService
{
    private readonly IMarketGateway marketGateway;

    public CotacaoService(IMarketGateway marketGateway)
    {
        this.marketGateway = marketGateway;
    }

    public async Task<IEnumerable<CotacaoResponse>> GetCotacoes(int totalPerPage)
    {
        var markets = await this.marketGateway.GetMarketsAsync(totalPerPage);

        return markets.Select(m => new CotacaoResponse
        {
            Id = m.Id,
            Nome = m.Name,
            Preco = (double)m.CurrentPrice,
            Simbolo = m.Symbol,
            ValorMercado = (double)m.MarketCap,
            Variacao24h = (double)m.MarketCapChange24h,
            Volume = (double)m.TotalVolume
        });
    }
}
