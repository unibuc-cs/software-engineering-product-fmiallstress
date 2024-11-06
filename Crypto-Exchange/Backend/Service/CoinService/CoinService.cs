using AutoMapper;
using System.Drawing;
using test_binance_api.Models;
using test_binance_api.Repository.CoinRepository;

namespace test_binance_api.Service.CoinService
{
    public class CoinService : ICoinService
    {

        private readonly ICoinRepository _coinRepository;
        public IMapper _mapper { get; set; }

        public CoinService(ICoinRepository coinRepository, IHttpClientFactory clientFactory, IMapper mapper)
        {
            _coinRepository = coinRepository;
            _mapper = mapper;
        }

        //live price of a pair
        public async Task<decimal> GetLivePrice(string pair)
        {
            var price = await _coinRepository.GetLivePrice(pair);
            return price;
        }

        //market capitalization of a pair
        public async Task<decimal> GetMarketCap(string pair)
        {
            var value = await _coinRepository.GetMarketCapAsync(pair);
            return value;
        }


        //price from past of a pair
        public async Task<decimal> GetHistoricalPrice(string pair, DateTime date)
        {
            var price = await _coinRepository.GetHistoricalPrice(pair, date);
            return price;
        }


        //range of prices from past of a pair
        public async Task<List<decimal>> GetPreviousPrices(string pair, DateTime date, int offset)

        {
            var prices = await _coinRepository.GetPreviousPrices(pair, date, offset);
            return prices;
        }

        //get rsi values for a pair
        public async Task<List<decimal>> CalculateLastRSIs(string pair, int offset, int amount)
        {
            var values = await _coinRepository.CalculateLastRSIs(pair, offset, amount);
            return values;
        }


        //create(+ getting live info)
        public async Task CreateCoin(CoinCreateDTO coin)
        {
            string? pair = coin.Symbol;
            var vcoin = _mapper.Map<Coin>(coin);
            vcoin.Price = await _coinRepository.GetLivePrice(pair);
            vcoin.MarketCap = await _coinRepository.GetMarketCapAsync(pair);
            await _coinRepository.CreateAsync(vcoin);
        }


        //get all
        public async Task<List<CoinShowDTO>> GetAll()
        {
            var coins = await _coinRepository.GetAllAsync();
            return _mapper.Map<List<CoinShowDTO>>(coins);
        }


        //update all info from database
        public async Task RefreshCoins()
        {
            var coins = await _coinRepository.GetAllAsync();
            foreach (var c in coins)
            {
                c.Symbol = c.Symbol.ToUpperInvariant();
                c.Price = await _coinRepository.GetLivePrice(c.Symbol);
                c.MarketCap = await _coinRepository.GetMarketCapAsync(c.Symbol);

                _coinRepository.Update(c);
            }

            await _coinRepository.SaveAsync();
        }



    }
}
