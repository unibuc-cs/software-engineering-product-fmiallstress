using Microsoft.AspNetCore.Mvc;
using test_binance_api.Service.CandleStickService;
using test_binance_api.Service.CoinService;
using test_binance_api.Service.TradingService;
using test_binance_api.Service.UserWalletService;

namespace test_binance_api.Controllers
{
    namespace test_binance_api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class TradingController : ControllerBase
        {
            private readonly ICoinService _coinService;
            private readonly IUserWalletService _userWalletService;
            private readonly ITradingService _tradingService;

            public TradingController(ICoinService coinService, IUserWalletService userWalletService, ITradingService tradingService)
            {
                _coinService = coinService;
                _userWalletService = userWalletService;
                _tradingService = tradingService;
            }


            [HttpGet("GetWallet/{userId}")]
            public async Task<IActionResult> GetWallet(Guid userId)
            {
                try
                {
                    var wallet = await _userWalletService.GetWalletByUserId(userId);

                    if (wallet == null)
                    {
                        return NotFound("Wallet not found");
                    }
                    Console.WriteLine($"controller Wallet Holdings: {wallet.CurrentHoldings.Count}");
                    return Ok(wallet);
                }
                catch (Exception ex)
                {
                    // Log the exception to see details
                    Console.WriteLine($"Error: {ex.Message}");
                    return BadRequest(ex.Message);
                }
            }


            [HttpGet("GetWalletEstimate/{userId}")]
            public async Task<IActionResult> GetEstimate(Guid userId)
            {
                try
                {
                    var estimateValue = await _tradingService.GetEstimate(userId);
                    return Ok(estimateValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("MoneyUserWallet/{userId}/{amount}")]
            public async Task<IActionResult> MoneyFromUserToWallet(Guid userId, decimal amount)
            {
                try
                {
                    var wallet = await _userWalletService.GetWalletByUserId(userId);

                    if (wallet == null)
                    {
                        return NotFound("Wallet not found");
                    }

                    await _userWalletService.MoneyFromUserToWallet(userId, amount);
                    return Ok();
                }
                catch (Exception ex)
                {
                    // Log the exception to see details
                    Console.WriteLine($"Error: {ex.Message}");
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("MoneyWalletUser/{userId}/{amount}")]
            public async Task<IActionResult> MoneyFromWalletToUser(Guid userId, decimal amount)
            {
                try
                {
                    var wallet = await _userWalletService.GetWalletByUserId(userId);

                    if (wallet == null)
                    {
                        return NotFound("Wallet not found");
                    }

                    await _userWalletService.MoneyFromWalletToUser(userId, amount);
                    return Ok();
                }
                catch (Exception ex)
                {
                    // Log the exception to see details
                    Console.WriteLine($"Error: {ex.Message}");
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("Buy/{userId}/{pair}/{amount}")]
            public async Task<IActionResult> Buy(Guid userId, string pair, decimal amount)
            {
                try
                {
                    await _tradingService.Buy(userId, pair, amount);
                    return Ok(); 
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("Sell/{userId}/{pair}/{amount}")]
            public async Task<IActionResult> Sell(Guid userId, string pair, decimal amount)
            {
                try
                {
                    await _tradingService.Sell(userId, pair, amount);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


        }


    }
}
