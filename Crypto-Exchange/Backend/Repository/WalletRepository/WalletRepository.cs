using Microsoft.Identity.Client;
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

        public void UpdateWalletBalance(Wallet wallet, decimal amount)
        {
            wallet.Balance += amount;
            Update(wallet);
        }
    }
}
