using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using test_binance_api.Controllers;
using Xunit;

namespace CryptoPredictTestProject.Controllers
{
    public class ChatGPTControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly ChatGPTController _controller;

        public ChatGPTControllerTest()
        {
            // Create a fake IConfiguration instance
            _configuration = A.Fake<IConfiguration>();

            // Create a fake IConfigurationSection instance
            var fakeSection = A.Fake<IConfigurationSection>();

            // Setup the fake section to return the desired API key
            A.CallTo(() => fakeSection.Value).Returns("my-apy-key-here"); // Replace with your actual API key or use a dummy value for testing

            // Setup the IConfiguration to return the fake section for the specific key
            A.CallTo(() => _configuration.GetSection("ApiKeys:OpenAIKey")).Returns(fakeSection);

            _controller = new ChatGPTController(_configuration);
        }

        [Fact]
        public async Task ChatGPTController_GetAIBasedResult_ReturnOK()
        {
            // This one will return BadRequest, because it's not testing the actual api key 

            // Arrange
            string searchText = "Hi";
            int maxTokens = 100;

            // Act
            var result = await _controller.GetAIBasedResult(searchText, maxTokens);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
    }
}
