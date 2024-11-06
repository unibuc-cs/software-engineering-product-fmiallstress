using test_binance_api.Models.DTOs.User;

namespace test_binance_api.Service.UserWalletHistoryService
{
    public interface IUserWalletHistoryService 
    {
        Task<UserDTO> CreateAsync(UserCreateDTO user);
    }
}
