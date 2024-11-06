using test_binance_api.Models.Base;
using test_binance_api.Models.DTOs;

namespace test_binance_api.Models
{
    public class Wallet : BaseEntity
    {
        public Guid? IdUser { get; set; }
        public User? User { get; set; }
        public ICollection<CoinDTO>? Transactions { get; set; }
    }
}
