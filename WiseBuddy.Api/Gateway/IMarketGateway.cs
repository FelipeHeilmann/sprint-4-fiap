using WiseBuddy.Api.Dto;

namespace WiseBuddy.Api.Gateway;

public interface IMarketGateway
{
    public Task<IEnumerable<MarketDto>> GetMarketsAsync(int totalPerPage);
}
