using Microsoft.AspNetCore.Identity;
using test_binance_api.Data;

namespace test_binance_api.Helpers.Seeders
{
    public class UserRoleSeeder
    {
        private readonly BinanceContext _binanceContext;

        public UserRoleSeeder(BinanceContext binanceContext)
        {
            _binanceContext = binanceContext;
        }

        public void SeedInitialUserRole()
        {
            if (!_binanceContext.UserRoles.Any())
            {
                var userrole1 = new IdentityUserRole<Guid>()
                {
                    RoleId = new Guid("581f995a-b150-4337-a473-e3ff50c3c784"),
                    UserId = new Guid("65006866-8d62-42d8-8d32-3494b5860b99")
                };

                var userrole2 = new IdentityUserRole<Guid>()
                {
                    RoleId = new Guid("aac5dc91-2f46-42e6-bc73-3d91594ca01b"),
                    UserId = new Guid("49f42890-0eef-417f-aa55-3fede394be8d")
                };

                _binanceContext.UserRoles.Add(userrole1);
                _binanceContext.UserRoles.Add(userrole2);
                _binanceContext.SaveChanges();

            }
        }
    }
}
