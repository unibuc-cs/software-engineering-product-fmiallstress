using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace test_binance_api.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? DeviceToken { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Precision(18, 8)]
        public decimal Balance { get; set; } = 0;
        public bool? Consent { get; set; } = false;


        public Guid? IdWallet { get; set; }
        public Wallet? Wallet { get; set; }


    }
}
