using test_binance_api.Models.Base;
using test_binance_api.Models.DTOs;

namespace test_binance_api.Models
{
    public class Wallet : BaseEntity
    {
        public Guid? IdUser { get; set; }
        public User? User { get; set; }
        public uint? Balance { get; set; } = 0;
        public ICollection<CoinDTO> CurrentHoldings { get; set; } = new List<CoinDTO>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
