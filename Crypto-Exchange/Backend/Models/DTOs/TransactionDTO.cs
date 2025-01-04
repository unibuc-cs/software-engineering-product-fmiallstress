namespace test_binance_api.Models.DTOs
{
    public class TransactionDTO
    {
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
