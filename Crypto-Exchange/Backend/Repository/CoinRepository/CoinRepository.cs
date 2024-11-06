using Newtonsoft.Json.Linq;
using test_binance_api.Models;
using test_binance_api.Repository.GenericRepository;
using test_binance_api.Data;
using test_binance_api.Models.Errors;

namespace test_binance_api.Repository.CoinRepository
{
    public class CoinRepository : GenericRepository<Coin>, ICoinRepository
    {
        public readonly IHttpClientFactory _clientFactory;

        public CoinRepository(BinanceContext binanceContext, IHttpClientFactory clientFactory) : base(binanceContext)
        {
            _clientFactory = clientFactory;
        }



        //used Binance REST Api to get the live price
        //must receive a valid pair <coin symbol + fiat currency>
        public async Task<decimal> GetLivePrice(string pair)
        {
            pair = pair.ToUpperInvariant();
            var apiUrl = $"https://api.binance.com/api/v3/ticker/price?symbol={pair}";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);
            var priceResponse = await response.Content.ReadFromJsonAsync<Coin>();

            return (decimal)priceResponse.Price;
        }


        //used CryptoCompare REST Api to get the market capitalization for a crypto currency
        //must receive a valid pair <coin symbol + fiat currency>
        //used ChatGPT for parsing
        public async Task<decimal> GetMarketCapAsync(string symbol)
        {

            symbol = symbol.ToUpperInvariant();
            string lastFourCharacters = symbol.Substring(symbol.Length - 4);

            if (lastFourCharacters.Equals("Usdt", StringComparison.OrdinalIgnoreCase))
            {
                // If the last 4 characters are "Usdt", delete them
                symbol =  symbol.Substring(0, symbol.Length - 4);
            }
            else if (symbol.Length >= 3)
            {
                // Get the last 3 characters of the input string
                string lastThreeCharacters = symbol.Substring(symbol.Length - 3);

                // Check if the last 3 characters are "eur", "usdt", or "ron"
                if (lastThreeCharacters.Equals("eur", StringComparison.OrdinalIgnoreCase) ||
                    lastThreeCharacters.Equals("usdt", StringComparison.OrdinalIgnoreCase) ||
                    lastThreeCharacters.Equals("ron", StringComparison.OrdinalIgnoreCase))
                {
                    // If yes, delete the last 3 characters
                    symbol =  symbol.Substring(0, symbol.Length - 3);
                }
            }

            string cryptoCompareApiUrl = $"https://min-api.cryptocompare.com/data/pricemultifull?fsyms={symbol}&tsyms=USD";

            var client = _clientFactory.CreateClient();

            // Get the market data from CryptoCompare
            var cryptoCompareResponse = await client.GetAsync(cryptoCompareApiUrl);
            if (!cryptoCompareResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {cryptoCompareResponse.ReasonPhrase}");
            }
            var cryptoCompareContent = await cryptoCompareResponse.Content.ReadAsStringAsync();
            var cryptoCompareJson = JObject.Parse(cryptoCompareContent);

            // Extract the market cap from the response
            try 
            {
                decimal marketCap = (decimal)cryptoCompareJson["RAW"][symbol]["USD"]["MKTCAP"];
                return marketCap;
            }
            catch (Exception ex)
            {
                throw new Exception("Pair not found");
            }
            
            
        }

        //used Binance REST Api to get price from the past
        //used ChatGPT for parsing
        //must receive a valid pair <coin symbol + fiat currency>
        public async Task<decimal> GetHistoricalPrice(string pair, DateTime date)
        {

            pair = pair.ToUpperInvariant();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.binance.com/");


                    string startTime = ((DateTimeOffset)date).ToUnixTimeMilliseconds().ToString();
                    string endTime = ((DateTimeOffset)date.AddDays(1)).ToUnixTimeMilliseconds().ToString();
                    string queryString = $"symbol={pair}&interval=1d&startTime={startTime}&endTime={endTime}&limit=1";


                    string requestUrl = $"api/v3/klines?{queryString}";


                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();


                    JArray jsonArray = JArray.Parse(responseBody);

                    if (jsonArray.Count == 0)
                    {
                        throw new Exception("No data available for the specified date.");
                    }


                    decimal historicalPrice = (decimal)jsonArray[0][4];

                    return historicalPrice;
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"HTTP Error: {e.Message}");
            }


        }

        //function that gets all the prices from a range(given date -> given date - offset)
        //using GetHistoricalPrice
        //must receive a valid pair <coin symbol + fiat currency> a valid date and a positive offset
        public async Task<List<decimal>> GetPreviousPrices(string pair, DateTime date, int offset)
        {
            List<decimal> prices = new List<decimal>();

            for (int i = 0; i < offset; i++)
            {
                var price = await GetHistoricalPrice(pair, date);
                date = date.AddDays(-1);
                prices.Add(price);
            }

            // Write to the console before returning prices
            Console.WriteLine("Prices fetched successfully:");
            foreach (var price in prices)
            {
                Console.WriteLine(price);
            }

            return prices;

        }


        //function that calculate the rsi index value for a range of prices
        public async Task<decimal> CalculateRSI(List<decimal> prices)
        {


            if (prices == null || prices.Count < 2)
                throw new ArgumentException("Invalid prices data.");

            decimal gainSum = 0;
            decimal lossSum = 0;

            // Calculate initial gain and loss
            for (int i = 1; i < prices.Count; i++)
            {
                decimal priceDiff = prices[i] - prices[i - 1];
                if (priceDiff >= 0)
                    gainSum += priceDiff;
                else
                    lossSum += Math.Abs(priceDiff);
            }

            decimal avgGain = gainSum / (prices.Count - 1);
            decimal avgLoss = lossSum / (prices.Count - 1);

            // Calculate RSI
            decimal rs = avgLoss != 0 ? avgGain / avgLoss : 0;
            decimal rsi = 100 - (100 / (1 + rs));


            return rsi;
        }

        //function that gets previous prices from today to a calculated date and retuns a list of rsi values
        //must receive a valid pair <coin symbol + fiat currency>
        //must receive a positive offset and amount
        public async Task<List<decimal>> CalculateLastRSIs(string pair, int offset, int amount)
        {
            List<decimal> values = new List<decimal>();

            DateTime date = DateTime.Now.AddDays(-1);

            var prices = await GetPreviousPrices(pair, date, offset);

            for(int k =0; k<prices.Count; k++)
                Console.Write(prices[k] + " ");
            Console.WriteLine("");

            var value = await CalculateRSI(prices);
            Console.WriteLine("value = " + value);
            values.Add(value);
            if (amount == 1)
                return values;
            else
            {
                date = date.AddDays(-offset);
                for (int i = 0; i < amount - 1; i++)
                {
                    prices.RemoveAt(0);
                    prices.Add(await GetHistoricalPrice(pair, date));

                    for (int k = 0; k < prices.Count; k++)
                        Console.Write(prices[k] + " ");
                    Console.WriteLine("");

                    value = await CalculateRSI(prices);
                    Console.WriteLine("value = " + value);
                    values.Add(value);
                    date = date.AddDays(-1);
                }

            }

            return values;

        }

    }
}

