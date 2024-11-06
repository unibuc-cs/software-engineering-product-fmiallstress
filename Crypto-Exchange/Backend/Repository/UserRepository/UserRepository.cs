using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test_binance_api.Models;

namespace test_binance_api.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userMananger)
        {
            _userManager = userMananger;
        }

        //create
        public async Task CreateAsync(User user)
        {
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Regular");
                return;
            }

            throw new Exception(result.Errors.First().Description);
        }


        //find
        public async Task<User> GetUserById(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }


        //delete
        public async Task Delete(Guid userId)
        {
            var existingUser = await GetUserById(userId);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            if ((await _userManager.DeleteAsync(existingUser)).Succeeded == false)
                throw new Exception("User delete failed");
        }


        //update
        public async Task Update(User user)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            if ((await _userManager.UpdateAsync(user)).Succeeded == false)
                throw new Exception("User update failed");
        }
    }
}
