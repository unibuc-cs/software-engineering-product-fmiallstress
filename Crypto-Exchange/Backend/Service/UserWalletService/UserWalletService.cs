﻿using AutoMapper;
using test_binance_api.Models;
using test_binance_api.Models.DTOs;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Models.Errors;
using test_binance_api.Repository.UserRepository;
using test_binance_api.Repository.WalletRepository;

namespace test_binance_api.Service.UserWalletService
{
    public class UserWalletService : IUserWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public UserWalletService(IUserRepository userRepository, IWalletRepository walletRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public async Task CreateUserWithWalletAsync(User user)
        {
            try
            {
                // Create the user
                await _userRepository.CreateAsync(user);

                // Create a wallet linked to the user
                var wallet = new Wallet
                {
                    Id = Guid.NewGuid(),
                    IdUser = user.Id,
                    Balance = 0,
                };
                Console.WriteLine(user.IdWallet);
                await _walletRepository.CreateAsync(wallet);
                user.IdWallet = wallet.Id;
                await _userRepository.Update(user); 

                Console.WriteLine($"user.idWallet {user.IdWallet}");
                Console.WriteLine($"wallet.Id {wallet.Id}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create user with wallet: {ex.Message}", ex);
            }
        }

        public async Task<WalletDTO> GetWalletByUserId(Guid id)
        {
            var user = await _userRepository.GetUserById(id);

            if (!user.IdWallet.HasValue)
            {
                throw new Exception("User does not have an associated wallet.");
            }

            var idWallet = user.IdWallet;
            var wallet = await _walletRepository.GetWalletWithCurrentHoldingsAsync(idWallet);
            var mappedWallet = _mapper.Map<WalletDTO>(wallet);

            return mappedWallet;
        }
            

        public async Task MoneyFromUserToWallet(Guid id, decimal amount)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user.Balance < amount + 1)
                    throw new Exception("Insufficient Funds!");


                var wallet = _walletRepository.FindById(user.IdWallet);
                var walletDTO = _mapper.Map<Wallet>(wallet);

                await _userRepository.UpdateUserBalance(user, -amount);
                _walletRepository.UpdateWalletBalance(walletDTO, amount-1);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send money from user to wallet: {ex.Message}", ex);
            }

        }

        public async Task MoneyFromWalletToUser(Guid id, decimal amount)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                var wallet = _walletRepository.FindById(user.IdWallet);

                if (wallet.Balance < amount + 1)
                    throw new Exception("Insufficient Funds!");

                var walletDTO = _mapper.Map<Wallet>(wallet);

                await _userRepository.UpdateUserBalance(user, amount-1);
                _walletRepository.UpdateWalletBalance(walletDTO, -amount);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send money from wallet to user: {ex.Message}", ex);
            }

        }
    }
}
