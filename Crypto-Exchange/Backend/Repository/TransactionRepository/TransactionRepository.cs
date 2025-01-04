using test_binance_api.Data;
using test_binance_api.Models;
using test_binance_api.Repository.AssetRepository;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.TransactionRepository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public readonly IHttpClientFactory _clientFactory;

        public TransactionRepository(BinanceContext binanceContext, IHttpClientFactory clientFactory) : base(binanceContext)
        {
            _clientFactory = clientFactory;
        }
    }
}
