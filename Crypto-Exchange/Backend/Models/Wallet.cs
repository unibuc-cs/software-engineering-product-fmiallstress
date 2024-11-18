using test_binance_api.Models.Base;
using test_binance_api.Models.DTOs;

namespace test_binance_api.Models
{
    public class Wallet : BaseEntity
    {
        public Guid? IdWallet { get; set; } 
        public Guid? IdUser { get; set; }
        public User? User { get; set; }
        public decimal Balance { get; set; } = 0;
        public ICollection<Asset> CurrentHoldings { get; set; } = new List<Asset>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
