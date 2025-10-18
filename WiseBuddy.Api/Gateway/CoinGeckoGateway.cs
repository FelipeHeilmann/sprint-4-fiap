using System.Text.Json;
using WiseBuddy.Api.Dto;

namespace WiseBuddy.Api.Gateway;

public class CoinGeckoGateway : IMarketGateway, IDisposable
{
    private readonly HttpClient _httpClient;

    public CoinGeckoGateway(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public void Dispose()
    {
        this._httpClient.Dispose();
    }

    public async Task<IEnumerable<MarketDto>> GetMarketsAsync(int totalPerPage)
    {
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = this.GetCoinGeckoUri(totalPerPage)
        };

        httpRequestMessage.Headers.Add("User-Agent", "WiseBuddyApp/1.0 (+https://wisebuddy.com)");

        var response = await this._httpClient.SendAsync(httpRequestMessage);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var markets = JsonSerializer.Deserialize<IEnumerable<MarketDto>>(content);

        return markets ?? throw new Exception();
    }

    private Uri GetCoinGeckoUri(int totalPage)
    {
        return new Uri($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=brl&order=market_cap_desc&per_page={totalPage}&page=1");
    }
}
