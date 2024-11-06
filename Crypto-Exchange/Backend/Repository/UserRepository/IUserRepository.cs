using test_binance_api.Models;

namespace test_binance_api.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetUserById(Guid id);
        Task<List<User>> GetUsersAsync();
        Task Delete(Guid id);
        Task Update(User user);
    }
}
