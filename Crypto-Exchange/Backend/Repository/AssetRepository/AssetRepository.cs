using test_binance_api.Data;
using test_binance_api.Models;
using test_binance_api.Repository.CoinRepository;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.AssetRepository
{
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        public readonly IHttpClientFactory _clientFactory;

        public AssetRepository(BinanceContext binanceContext, IHttpClientFactory clientFactory) : base(binanceContext)
        {
            _clientFactory = clientFactory;
        }
    }
    
}
