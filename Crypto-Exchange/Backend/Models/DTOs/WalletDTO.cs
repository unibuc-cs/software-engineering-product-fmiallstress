namespace test_binance_api.Models.DTOs
{
    public class WalletDTO
    {
        public decimal Balance { get; set; } = 0;
        public ICollection<CoinDTO> CurrentHoldings { get; set; } = new List<CoinDTO>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
