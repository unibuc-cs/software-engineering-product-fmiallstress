using Microsoft.AspNetCore.Identity;

namespace test_binance_api.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? DeviceToken { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal? Balance { get; set; }
        public bool? Consent { get; set; }

        public Guid? IdHistory { get; set; }
        public History? History { get; set; }

        public Guid? IdWallet { get; set; }
        public Wallet? Wallet { get; set; }


    }
}
