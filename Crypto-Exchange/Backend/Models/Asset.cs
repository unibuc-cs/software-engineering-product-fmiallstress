using Microsoft.EntityFrameworkCore;
using test_binance_api.Models.Base;

namespace test_binance_api.Models
{
    public class Asset : BaseEntity
    {
        public string? Symbol { get; set; }
        [Precision(18, 8)]
        public decimal? Amount { get; set; }
        public Guid? IdWallet { get; set; }
    }
}
