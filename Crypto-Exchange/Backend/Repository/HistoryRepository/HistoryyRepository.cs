using test_binance_api.Data;
using test_binance_api.Models;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.HistoryRepository
{
    public class HistoryyRepository : GenericRepository<History>, IHistoryyRepository
    {
        public HistoryyRepository(BinanceContext binanceContext) : base(binanceContext)
        {
        }
       
    }
}

