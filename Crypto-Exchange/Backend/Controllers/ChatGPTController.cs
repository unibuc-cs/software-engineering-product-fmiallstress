using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using test_binance_api.Service.CoinService;
using System.Globalization;
using OpenAI_API.Moderation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test_binance_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly ICoinService _coinService;

        public ChatGPTController(IConfiguration configuration, ICoinService coinService)
        {
            Configuration = configuration;
            _coinService = coinService;
        }

        [HttpPost]
        public async Task<string> GetAIBasedResult(string searchText, int maxTokens)
        {
            try
            {
                // Connection key for OpenAI API
                var apiKey = Configuration["ApiKeys:OpenAIKey"];

                string answer = string.Empty;

                var openai = new OpenAIAPI(apiKey);
                CompletionRequest completion = new CompletionRequest
                {
                    Prompt = searchText,                                      // text to search
                    Model = OpenAI_API.Models.Model.ChatGPTTurboInstruct,     // model to ask
                    MaxTokens = maxTokens                                     // max tokens of a batch (depending on model)
                };

                // Search for result
                var result = await openai.Completions.CreateCompletionsAsync(completion);
                if (result.Completions.Count > 0)
                {
                    // Get the first completion's text
                    answer = result.Completions[0].Text.Trim();
                }

                return answer;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        [HttpGet("GetPrediction/{pair}/{offset}/{rsiValue}/{noPreds}")]
        public async Task<IActionResult> GetPreviousPrices(string pair, int offset, int rsiValue, int noPreds)
        {
            try
            {
                var today = DateTime.Now;
                DateTime date = new DateTime(today.Year, today.Month, today.Day);
                var prices = await _coinService.GetPreviousPrices(pair, date, offset);
                var rsi = await _coinService.CalculateLastRSIs(pair, offset, rsiValue);

                // Convert the lists to string format
                string rsiValues = string.Join(", ", rsi);
                string priceValues = string.Join(", ", prices);


                var statement = "these are the rsi values of a currency [" + rsiValues +
                                    "], and this are the last real values of a currency: [" +
                                    priceValues +
                                    "] (head of the value is the most recent price). What do you think are the next " +
                                    noPreds +
                                    " prices? Please give me only the list of decimals in CSV format, with no other additional information like text and explanation";


                string values = await GetAIBasedResult(statement, 1000);

                var _list = values.Split(",");

                List<decimal> list = new List<decimal>();
                foreach (var x in _list)
                {
                    var v = x.Split(".");
                    int no0 = int.Parse(v[0]);
                    int no1 = int.Parse(v[1]);
                    decimal number = no0 + Convert.ToDecimal(no1) / (decimal)Math.Pow(10, Math.Floor(Math.Log10(no1) + 1));
                    list.Add(number);
                }


                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
