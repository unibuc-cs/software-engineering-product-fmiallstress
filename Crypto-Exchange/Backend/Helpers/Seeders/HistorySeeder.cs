using Microsoft.AspNetCore.Identity;
using test_binance_api.Data;
using test_binance_api.Models;

namespace test_binance_api.Helpers.Seeders
{
    public class HistorySeeder
    {
        private readonly BinanceContext _binanceContext;

        public HistorySeeder(BinanceContext binanceContext)
        {
            _binanceContext = binanceContext;
        }

        public void SeedInitialUsers()
        {
            if (!_binanceContext.Histories.Any())
            {

                var history1 = new History
                {
                    Id = new Guid("FA81F9B4 - AC94 - 4895 - 895E-305654633B3C")
                };


                _binanceContext.Histories.Add(history1);
                _binanceContext.SaveChanges();
            }
        }
    }
}
