using test_binance_api.Models;
using test_binance_api.Models.DTOs;

namespace test_binance_api.Service.UserWalletService
{
    public interface IUserWalletService
    {
        Task CreateUserWithWalletAsync(User user);
        Task<WalletDTO> GetWalletByUserId(Guid id);
        Task MoneyFromUserToWallet(Guid id, decimal amount);
        Task MoneyFromWalletToUser(Guid id, decimal amount);
    }
}
