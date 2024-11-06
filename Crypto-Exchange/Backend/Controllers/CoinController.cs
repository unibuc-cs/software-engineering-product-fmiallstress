using Microsoft.AspNetCore.Mvc;
using Binance.Spot;
using Microsoft.AspNetCore.Http.HttpResults;
using test_binance_api.Service.CoinService;
using test_binance_api.Models;
using System.ComponentModel;
using System.Reflection;
using test_binance_api.Service.CandleStickService;
using test_binance_api.Models.Errors;

namespace test_binance_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;
        private readonly ICandleStickService _candleStickService;

        public CoinController(ICoinService coinService, ICandleStickService candleStickService)
        {
            _coinService = coinService;
            _candleStickService = candleStickService;
        }

        [HttpGet("LivePrice/{pair}")]
        public async Task<IActionResult> GetLivePrice(string pair)
        {
            try
            {
                var price = await _coinService.GetLivePrice(pair);
                return Ok(price);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("LiveMarketCap/{pair}")]
        public async Task<IActionResult> GetMarketCap(string pair)
        {
            try
            {
                var value = await _coinService.GetMarketCap(pair);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PreviousPrice/{pair}/{day}/{month}/{year}")]
        public async Task<IActionResult> GetHistoricalPrice(string pair, int day, int month, int year)
        {
            try
            {
                DateTime date = new DateTime(year, month, day);
                var price = await _coinService.GetHistoricalPrice(pair, date);
                return Ok(price);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PreviousPrices/{pair}/{day}/{month}/{year}/{offset}")]
        public async Task<IActionResult> GetPreviousPrices(string pair, int day, int month, int year, int offset)
        {
            try
            {
                DateTime date = new DateTime(year, month, day);
                var prices = await _coinService.GetPreviousPrices(pair, date, offset);
                return Ok(prices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CalculateRSIs/{pair}/{offset}/{amount}")]
        public async Task<IActionResult> CalculateLastRSIs(string pair, int offset, int amount)
        {
            try
            {
                var values = await _coinService.CalculateLastRSIs(pair, offset, amount);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Candle/{pair}/{day}/{month}/{year}/{interval}")]
        public async Task<IActionResult> GetCandleStickData(string pair, int day, int month, int year, string interval)
        {
            try
            {
                DateTime date = new DateTime(year, month, day);
                var candleStick = await _candleStickService.GetCandleStickData(pair, interval, date);
                return Ok(candleStick);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Candles/{pair}/{interval}/{day}/{month}/{year}/{day2}/{month2}/{year2}")]
        public async Task<IActionResult> GetAllCandles(string pair, string interval, int day, int month, int year, int day2, int month2, int year2)
        {
            try
            {
                DateTime startDate = new DateTime(year, month, day);
                DateTime endDate = new DateTime(year2, month2, day2);
                var candleStick = await _candleStickService.GetAllCandles(pair, interval, startDate, endDate);
                return Ok(candleStick);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCoins()
        {
            var coins = await _coinService.GetAll();
            return Ok(coins);
        }

        [HttpPost("CreateCoin")]
        public async Task<IActionResult> AddCoin(CoinCreateDTO coin)
        {
            try 
            {
                await _coinService.CreateCoin(coin);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 500,
                    Message = exception.Message
                });
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                await _coinService.RefreshCoins();
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 500,
                    Message = exception.Message
                });
            }
        }
    }
}

