using Binance.Spot;
using Microsoft.AspNetCore.Identity;
using test_binance_api.Data;
using test_binance_api.Models;

namespace test_binance_api.Helpers.Seeders
{
    public class UserSeeder
    {
        private readonly BinanceContext _binanceContext;

        public UserSeeder(BinanceContext binanceContext)
        {
            _binanceContext = binanceContext;
        }

        public void SeedInitialUsers()
        {
            if (!_binanceContext.Users.Any())
            {

                var hasher = new PasswordHasher<User>();

                var user1 = new User
                {
                    Id = new Guid("49f42890-0eef-417f-aa55-3fede394be8d"),
                    FirstName = "Mircea",
                    LastName = "Razvan",
                    UserName = "Qarty26",
                    NormalizedUserName = "Qarty26".ToUpper(),
                    Email = "rzvandmir03@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "razpassword"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                };


                var user2 = new User
                {
                    Id = new Guid("65006866-8d62-42d8-8d32-3494b5860b99"),
                    FirstName = "Dogaru",
                    LastName = "Mihai",
                    UserName = "Matoka26",
                    NormalizedUserName = "Matoka26".ToUpper(),
                    Email = "migai26@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "mihaipassword"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                _binanceContext.Users.Add(user1);
                _binanceContext.Users.Add(user2);
                _binanceContext.SaveChanges();
            }
        }
    }
}
