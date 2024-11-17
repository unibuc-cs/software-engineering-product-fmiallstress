using test_binance_api.Models;

namespace test_binance_api.Service.UserWalletService
{
    public interface IUserWalletService
    {
        Task CreateUserWithWalletAsync(User user);
    }
}
