using test_binance_api.Models;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.CoinRepository
{
    public interface ICoinRepository : IGenericRepository<Coin>
    {
        Task<decimal> GetLivePrice(string pair);
        Task<decimal> GetHistoricalPrice(string pair, DateTime date);
        Task<List<decimal>> GetPreviousPrices(string pair, DateTime date, int offset);
        Task<List<decimal>> CalculateLastRSIs(string pair, int offset, int amount);
        Task<decimal> GetMarketCapAsync(string tradingPair);
    }
}
