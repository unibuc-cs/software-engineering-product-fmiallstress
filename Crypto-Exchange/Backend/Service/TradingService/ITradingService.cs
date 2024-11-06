using test_binance_api.Models;

namespace test_binance_api.Service.TradingService
{
    public interface ITradingService
    {
        Task<Coin> Trade(Guid idUser, string type, string pair, decimal amount);
    }
}
