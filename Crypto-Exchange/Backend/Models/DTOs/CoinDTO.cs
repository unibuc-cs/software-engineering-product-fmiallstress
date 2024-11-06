using test_binance_api.Models.Base;

namespace test_binance_api.Models.DTOs
{
    public class CoinDTO : BaseEntity
    {
        public string? Symbol { get; set; }
        public decimal? Amount { get; set; }

    }
}
