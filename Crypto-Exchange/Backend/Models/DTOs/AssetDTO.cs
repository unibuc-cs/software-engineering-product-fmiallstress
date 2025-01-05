using Microsoft.EntityFrameworkCore;

namespace test_binance_api.Models.DTOs
{
    public class AssetDTO
    {
        public string? Symbol { get; set; }
        [Precision(18, 8)]
        public decimal? Amount { get; set; }
    }
}
