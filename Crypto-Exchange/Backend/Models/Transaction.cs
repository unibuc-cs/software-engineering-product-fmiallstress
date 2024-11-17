using test_binance_api.Models.Base;

namespace test_binance_api.Models
{
    public class Transaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }

        public decimal Amount { get; set; } // Positive for buy, negative for sell
        public decimal Price { get; set; } // Price per coin at transaction time
        public string Type { get; set; } // "Buy" or "Sell"
        public DateTime TransactionDate { get; set; } // Date of transaction
    }
}