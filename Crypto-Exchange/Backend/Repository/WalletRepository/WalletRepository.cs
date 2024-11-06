using test_binance_api.Data;
using test_binance_api.Models;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.WalletRepository
{
    public class WalletRepository : GenericRepository<Wallet> , IWalletRepository
    {
        public WalletRepository(BinanceContext binanceContext) : base(binanceContext)
        {
        }
    }
}
