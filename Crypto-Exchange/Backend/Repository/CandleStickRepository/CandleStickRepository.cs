using Newtonsoft.Json.Linq;
using test_binance_api.Models;

namespace test_binance_api.Repository.CandleStickRepository
{
    public class CandleStickRepository : ICandleStickRepository
    {
        public readonly IHttpClientFactory _clientFactory;

        public CandleStickRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        //function that get all the data from a single candlestick (market data within a given interval) from a certain date
        //must receive a valid pair <coin symbol + fiat currency>
        //must receive a valid StartDate
        //must receive a valid interval (1m, 15m, 1h, 3d, 1w, etc.)
        //parsed using ChatGPT
        public async Task<CandleStick>? GetCandleStickData(string pair, string interval, DateTime startDate)
        {
            pair = pair.ToUpperInvariant();
            long startTime = ((DateTimeOffset)startDate).ToUnixTimeMilliseconds();
            var apiUrl = $"https://api.binance.com/api/v3/klines?symbol={pair}&interval={interval}&startTime={startTime}";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var dataResponse = await response.Content.ReadAsStringAsync();
            var jsonArray = JArray.Parse(dataResponse);

            if (jsonArray.Count == 0)
                return null;

            var firstCandle = jsonArray[0];
            var candleStick = new CandleStick
            {
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds((long)firstCandle[0]).DateTime,
                OpenPrice = (decimal)firstCandle[1],
                HighPrice = (decimal)firstCandle[2],
                LowPrice = (decimal)firstCandle[3],
                ClosePrice = (decimal)firstCandle[4]
            };

            return candleStick;
        }


        //function that gets information about all the candles within a range of dates
        //using GetCandleStickData()
        public async Task<List<CandleStick>>? GetAllCandles(string pair, string interval, DateTime startDate, DateTime endDate)
        {
            var number = int.Parse(interval.Substring(0, interval.Length - 1));
            string timeframe = interval[interval.Length - 1] + "";
            timeframe = timeframe.ToLowerInvariant();
            var candles = new List<CandleStick>();

            while (startDate <= endDate)
            {
                var candle = await GetCandleStickData(pair, interval, startDate);
                candles.Add(candle);

                if (timeframe.Equals("s"))
                    startDate = startDate.AddSeconds(number);
                else if (timeframe.Equals("m"))
                    startDate = startDate.AddMinutes(number);
                else if (timeframe.Equals("h"))
                    startDate = startDate.AddHours(number);
                else if (timeframe.Equals("d"))
                    startDate = startDate.AddDays(number);
                else if (timeframe.Equals("w"))
                    startDate = startDate.AddDays(7 * number);
            }

            return candles;
        }

    }
}
