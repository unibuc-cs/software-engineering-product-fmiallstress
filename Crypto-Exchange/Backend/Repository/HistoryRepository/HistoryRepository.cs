using test_binance_api.Data;
using test_binance_api.Models;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.HistoryRepository
{
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {

        public HistoryRepository(BinanceContext binanceContext) : base(binanceContext) { }
    }
}
