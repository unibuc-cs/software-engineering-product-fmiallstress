using test_binance_api.Models.Base;

namespace test_binance_api.Models
{
    public class CandleStick
    {
        public DateTime? OpenTime { get; set; }
        public decimal? OpenPrice { get; set; }
        public decimal? LowPrice { get; set; }
        public decimal? HighPrice { get; set; }
        public decimal? ClosePrice { get; set; }
    }
}
