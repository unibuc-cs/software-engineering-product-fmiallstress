namespace test_binance_api.Models.DTOs
{
    public class WalletDTO
    {
        public decimal Balance { get; set; } = 0;
        public ICollection<AssetDTO> CurrentHoldings { get; set; } = new List<AssetDTO>();
        public ICollection<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
    }
}
