using test_binance_api.Models;

namespace test_binance_api.Service.TradingService
{
    public interface ITradingService
    {
        Task Buy(Guid idUser, string pair, decimal amount);
        Task Sell(Guid idUser, string pair, decimal amount);
    }
}
