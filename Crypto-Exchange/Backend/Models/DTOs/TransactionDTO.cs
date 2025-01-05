using Microsoft.EntityFrameworkCore;

namespace test_binance_api.Models.DTOs
{
    public class TransactionDTO
    {
        public string Symbol { get; set; }
        [Precision(18, 8)]
        public decimal Amount { get; set; }
        [Precision(18, 8)]
        public decimal Price { get; set; }
        public string Type { get; set; }
        [Precision(18, 8)]
        public decimal Fee { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
