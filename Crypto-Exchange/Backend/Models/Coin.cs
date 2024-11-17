using test_binance_api.Models.Base;

namespace test_binance_api.Models
{
    public class Coin : BaseEntity
    {
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }
        public decimal? MarketCap { get; set; }
        public string? Field { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Type { get; set; } //buy or sell

    }
}
