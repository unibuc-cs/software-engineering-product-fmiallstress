using test_binance_api.Models.DTOs.User;
using test_binance_api.Models.Errors;

namespace test_binance_api.Service.UserService
{
    public interface IUserService
    {
        Task<UserDTO> GetUserById(Guid id);
        Task<UserDTO> CreateAsync(UserCreateDTO user);
        Task Delete(Guid id);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> Update(UserUpdateDTO user);
        Task<Guid> Login(LoginDTO loginDto);
        Task Logout();
        //Task<ErrorResponse> SignUp(UserSignUpDTO signup);
        //Task<ErrorResponse> ConfirmEmail(string email, string token);
        Task StoreDeviceToken(string IdUser, string deviceToken);
    }
}
