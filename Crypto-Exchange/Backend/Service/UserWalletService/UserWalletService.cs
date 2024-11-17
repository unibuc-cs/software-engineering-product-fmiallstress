using test_binance_api.Models;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Repository.UserRepository;
using test_binance_api.Repository.WalletRepository;

namespace test_binance_api.Service.UserWalletService
{
    public class UserWalletService : IUserWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public UserWalletService(IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
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
                    IdUser = user.Id,
                    Balance = 0, // Initial balance
                };

                await _walletRepository.CreateAsync(wallet);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create user with wallet: {ex.Message}", ex);
            }
        }
    }
}
