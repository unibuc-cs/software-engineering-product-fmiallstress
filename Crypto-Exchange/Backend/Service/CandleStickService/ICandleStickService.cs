using test_binance_api.Models;

namespace test_binance_api.Service.CandleStickService
{
    public interface ICandleStickService
    {
        Task<CandleStick>? GetCandleStickData(string pair, string interval, DateTime startDate);
        Task<List<CandleStick>>? GetAllCandles(string pair, string interval, DateTime startDate, DateTime endDate);
    }
}
