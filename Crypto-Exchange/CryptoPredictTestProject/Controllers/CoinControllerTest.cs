using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using test_binance_api.Repository.CoinRepository;
using test_binance_api.Service.CandleStickService;
using test_binance_api.Service.CoinService;

namespace CryptoPredictTestProject.Controllers
{
    public class CoinControllerTest
    {
        private readonly ICoinService _coinService;
        private readonly ICandleStickService _candleStickService;
        private readonly test_binance_api.Controllers.CoinController _controller;
        public CoinControllerTest()
        {
            _coinService = A.Fake<ICoinService>();
            _candleStickService = A.Fake<ICandleStickService>();
            _controller = new test_binance_api.Controllers.CoinController(_coinService, _candleStickService);
        }

        [Fact]
        public async void CoinController_GetLivePrice_ReturnOK()
        {
            //Arrange
            string pair = "ETHUSST";

            //Act
            var result = await _controller.GetLivePrice(pair);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void CoinController_CalculateLastRSIs_ReturnOK()
        {
            //Arrange 
            string pair = "xrpusdt";
            int offset = 5;
            int amount = 5;

            //Act
            var result = await _controller.CalculateLastRSIs(pair, offset, amount);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void CoinController_GetHistoricalPrice_ReturnOK()
        {
            //Arrange 
            string pair = "egldusdt";
            int day = 1;
            int month = 2;
            int year = 2023;

            //Act
            var result = await _controller.GetHistoricalPrice(pair, day, month, year);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void CoinController_GetPreviousPrices_ReturnOK()
        {
            //Arrange 
            string pair = "xrpusdt";
            int day = 1;
            int month = 2;
            int year = 2023;
            int offset = 5;

            //Act
            var result = await _controller.GetPreviousPrices(pair, day, month, year, offset);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
