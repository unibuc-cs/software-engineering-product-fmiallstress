using Microsoft.AspNetCore.Identity;
using System.Drawing;
using test_binance_api.Data;
using test_binance_api.Models;

namespace test_binance_api.Helpers.Seeders
{
    public class CoinSeeder
    {

        private readonly BinanceContext _binanceContext;

        public CoinSeeder(BinanceContext binanceContext)
        {
            _binanceContext = binanceContext;
        }

        public void SeedInitialCoins()
        {
            if (!_binanceContext.Coins.Any())
            {

                var coin1 = new Coin
                {
                    Name = "BTC",
                    Symbol = "BTCUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin2 = new Coin
                {
                    Name = "FTM",
                    Symbol = "FTMUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin3 = new Coin
                {
                    Name = "EGLD",
                    Symbol = "EGLDUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin4 = new Coin
                {
                    Name = "SOL",
                    Symbol = "SOLUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin5 = new Coin
                {
                    Name = "DOGE", 
                    Symbol = "DOGEUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin6 = new Coin
                {
                    Name = "BNB",
                    Symbol = "BNBUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin7 = new Coin
                {
                    Name = "FIL",
                    Symbol = "FILUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin8 = new Coin
                {
                    Name = "MATIC",
                    Symbol = "MATICUSDT",
                    Price = 0,
                    MarketCap = 0
                };

                var coin9 = new Coin
                {
                    Name = "ETH",
                    Symbol = "ETHUSDT",
                    Price = 0,
                    MarketCap = 0
                };


                _binanceContext.Coins.Add(coin1);
                _binanceContext.Coins.Add(coin2);
                _binanceContext.Coins.Add(coin3);
                _binanceContext.Coins.Add(coin4);
                _binanceContext.Coins.Add(coin5);
                _binanceContext.Coins.Add(coin6);
                _binanceContext.Coins.Add(coin7);
                _binanceContext.Coins.Add(coin8);
                _binanceContext.Coins.Add(coin9);

                _binanceContext.SaveChanges();
            }
        }
    }
    
}
