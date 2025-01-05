using AutoMapper;
using System.ComponentModel;
using test_binance_api.Models;
using test_binance_api.Models.DTOs;
using test_binance_api.Repository.AssetRepository;
using test_binance_api.Repository.CoinRepository;
using test_binance_api.Repository.TransactionRepository;
using test_binance_api.Repository.UserRepository;
using test_binance_api.Repository.WalletRepository;

namespace test_binance_api.Service.TradingService
{
    public class TradingService : ITradingService
    {

        private readonly ICoinRepository _coinRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        private readonly IAssetRepository _assetRepository;
        private readonly ITransactionRepository _transactionRepository;




        public TradingService(
            ICoinRepository coinRepository,
            IUserRepository userRepository, 
            IWalletRepository walletRepository,
            IMapper mapper,
            IAssetRepository assetRepository,
            ITransactionRepository transactionRepository)
        {
            _coinRepository = coinRepository;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _mapper = mapper;
            _assetRepository = assetRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task Buy(Guid idUser, string pair, decimal amount)
        {
            var price = await _coinRepository.GetLivePrice(pair);
            var user = await _userRepository.GetUserById(idUser);
            var idWallet = user.IdWallet;
            var wallet = await _walletRepository.GetWalletWithCurrentHoldingsAsync(idWallet);

            if (wallet == null)
                throw new Exception("Wallet not found!");

            if (wallet.Balance < amount)
                throw new Exception("Insufficient balance!");

            var position_size = amount / price;
            var fee = position_size * ((decimal)(0.05) / 100);
            position_size -= fee;
            wallet.Balance -= amount;

            var ownPair = wallet.CurrentHoldings.FirstOrDefault(c => c.Symbol.Equals(pair, StringComparison.OrdinalIgnoreCase));

            if (ownPair != null)
            {
                ownPair.Amount += position_size;
                _assetRepository.Update(ownPair);
            }
            else
            {
                var newPair = new Asset
                {
                    Id = Guid.NewGuid(),
                    Symbol = pair,
                    Amount = position_size,
                    IdWallet = idWallet,
                };


                wallet.CurrentHoldings.Add(newPair);
                await _assetRepository.CreateAsync(newPair);
            }

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Symbol = pair,
                Amount = position_size,
                Price = price,
                Type = "BUY",
                TransactionDate = DateTime.Now,
                IdWallet = idWallet,
                Fee = fee
            };
            wallet.Transactions.Add(transaction);
            await _transactionRepository.CreateAsync(transaction);

            _walletRepository.Update(wallet);
        }

        public async Task Sell(Guid idUser, string pair, decimal amount)
        {
            var price = await _coinRepository.GetLivePrice(pair);
            var user = await _userRepository.GetUserById(idUser);
            var idWallet = user.IdWallet;
            var wallet = await _walletRepository.GetWalletWithCurrentHoldingsAsync(idWallet);

            if (wallet == null)
                throw new Exception("Wallet not found!");

            var position_size = amount / price;
            var fee = position_size * ((decimal)(0.05) / 100);
            position_size += fee;

            var ownPair = wallet.CurrentHoldings.FirstOrDefault(c => c.Symbol.Equals(pair, StringComparison.OrdinalIgnoreCase));

            if (ownPair == null)
                throw new Exception("You do not hold this asset!");

            if (ownPair.Amount < position_size)
                throw new Exception("Your SELL position is bigger than the asset size!");

            ownPair.Amount -= position_size;
            wallet.Balance += amount;

            if (ownPair.Amount == 0)
            {
                wallet.CurrentHoldings.Remove(ownPair);
                _assetRepository.Delete(ownPair);
            }
            else _assetRepository.Update(ownPair);

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Symbol = pair,
                Amount = position_size,
                Price = price,
                Type = "SELL",
                TransactionDate = DateTime.Now,
                IdWallet = idWallet,
                Fee = fee
            };
            wallet.Transactions.Add(transaction);
            await _transactionRepository.CreateAsync(transaction);

            _walletRepository.Update(wallet);
        }


    }
}
