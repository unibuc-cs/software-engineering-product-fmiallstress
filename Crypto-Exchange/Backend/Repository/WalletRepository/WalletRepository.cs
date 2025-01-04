using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.CodeAnalysis;
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


        public Wallet GetWalletWithCurrentHoldings(Guid walletId)
        {
            return _binanceContext.Wallets
            .Include(w => w.CurrentHoldings)
            .Include(w => w.Transactions)
            .FirstOrDefault(w => w.Id == walletId);
        }

        public async Task<Wallet> GetWalletWithCurrentHoldingsAsync(Guid? walletId)
        { 
            return await _binanceContext.Wallets
                .Include(w => w.CurrentHoldings)
                .Include(w => w.Transactions)
                .FirstOrDefaultAsync(w => w.Id == walletId);
        }

    }
}
