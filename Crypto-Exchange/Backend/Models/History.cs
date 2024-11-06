using Microsoft.AspNetCore.Identity;
using test_binance_api.Models.Base;

namespace test_binance_api.Models
{
    public class History : BaseEntity
    {
        public Guid? IdUser { get; set; } 
        public User? User { get; set; }
        public ICollection<Coin>? Transactions { get; set; }
    }
}
