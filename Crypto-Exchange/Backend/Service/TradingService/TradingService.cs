using test_binance_api.Models;
using test_binance_api.Repository.CoinRepository;
using test_binance_api.Repository.UserRepository;

namespace test_binance_api.Service.TradingService
{
    public class TradingService : ITradingService
    {

        ICoinRepository _coinRepository;
        IUserRepository _userRepository;

        public TradingService(ICoinRepository coinRepository, IUserRepository userRepository)
        {
            _coinRepository = coinRepository;
            _userRepository = userRepository;
        }

        public async Task<Coin> Trade(Guid idUser, string type, string pair, decimal amount)
        {

            type = type.ToUpperInvariant();
            if (type != "BUY" && type != "SELL")
                return null;

            var price = await _coinRepository.GetLivePrice(pair);
            var user = await _userRepository.GetUserById(idUser);

            // to be replaced
            /*            if (price != 0)
                        {
                            Coin trade = new Coin
                            {
                                Id = new Guid(),
                                Symbol = pair,
                                Price = price,
                                Amount = amount,
                                Field = null,
                                DateTime = DateTime.Now,
                                Type = type,
                                IdHistory = null,
                                History = null

                            };

                            return trade;
                        }
                        else return null;*/

            return null;

        }

    }
}
