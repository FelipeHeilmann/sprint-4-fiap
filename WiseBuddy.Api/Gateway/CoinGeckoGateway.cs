using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using WiseBuddy.Api.Dto;

namespace WiseBuddy.Api.Gateway;

public class CoinGeckoGateway : IMarketGateway, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(40);

    public CoinGeckoGateway(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public async Task<IEnumerable<MarketDto>> GetMarketsAsync(int totalPerPage)
    {
        var cacheKey = $"markets_{totalPerPage}";

        if (_cache.TryGetValue(cacheKey, out IEnumerable<MarketDto>? cachedMarkets) && cachedMarkets is not null)
        {
            return cachedMarkets;
        }

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = this.GetCoinGeckoUri(totalPerPage)
        };

        httpRequestMessage.Headers.Add("User-Agent", "WiseBuddyApp/1.0 (+https://sprint-4-fiap.onrender.com)");

        var response = await _httpClient.SendAsync(httpRequestMessage);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var markets = JsonSerializer.Deserialize<IEnumerable<MarketDto>>(content) 
                      ?? throw new Exception("Falha ao desserializar os mercados");

        _cache.Set(cacheKey, markets, _cacheDuration);

        return markets;
    }

    private Uri GetCoinGeckoUri(int totalPage)
    {
        return new Uri($"https://api.coingecko.com/api/v3/coins/markets?vs_currency=brl&order=market_cap_desc&per_page={totalPage}&page=1&x_cg_demo_api_key=CG-LpmGsUHYNi376ZYFvNffFYa1");
    }
}
