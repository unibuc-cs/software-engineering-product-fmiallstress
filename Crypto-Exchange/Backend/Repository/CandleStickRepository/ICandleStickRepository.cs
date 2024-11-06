using test_binance_api.Models;

namespace test_binance_api.Repository.CandleStickRepository
{
    public interface ICandleStickRepository
    {
        Task<CandleStick>? GetCandleStickData(string pair, string interval, DateTime startDate);
        Task<List<CandleStick>>? GetAllCandles(string pair, string interval, DateTime startDate, DateTime endDate);
    }
}
