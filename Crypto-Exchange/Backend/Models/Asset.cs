using test_binance_api.Models.Base;

namespace test_binance_api.Models
{
    public class Asset : BaseEntity
    {
        public string? Symbol { get; set; }
        public decimal? Amount { get; set; }
        public Guid? IdWallet { get; set; }
    }
}
