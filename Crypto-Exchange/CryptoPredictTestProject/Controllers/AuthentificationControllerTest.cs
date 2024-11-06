using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Mailing.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Threading.Tasks;
using test_binance_api.Models;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Models.Errors;
using test_binance_api.Service.UserService;
using Xunit;

namespace CryptoPredictTestProject.Controllers
{
    public class AuthentificationControllerTest
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly test_binance_api.Controllers.AuthentificationController _controller;

        public AuthentificationControllerTest()
        {
            _userManager = A.Fake<UserManager<User>>();
            _userService = A.Fake<IUserService>();
            _emailService = A.Fake<IEmailService>();
            _mapper = A.Fake<IMapper>();
            _controller = new test_binance_api.Controllers.AuthentificationController(_userManager,
                _userService, _emailService, _mapper);
        }



/*      // this one was made with chat gpt
        [Fact]
        public async Task AuthentificationController_SignUp_ReturnOk()
        {
            // Arrange
            var signUpDto = new UserSignUpDTO { Email = "newuser@example.com" };
            var user = new User { Email = signUpDto.Email };
            var identityResult = IdentityResult.Success;

            // Set up mock responses
            A.CallTo(() => _userManager.FindByEmailAsync(signUpDto.Email)).Returns(Task.FromResult<User>(null));
            A.CallTo(() => _mapper.Map<User>(signUpDto)).Returns(user);
            A.CallTo(() => _userManager.CreateAsync(user)).Returns(Task.FromResult(identityResult));
            A.CallTo(() => _userManager.AddToRoleAsync(user, "Regular")).Returns(Task.FromResult(identityResult));
            A.CallTo(() => _userManager.GenerateEmailConfirmationTokenAsync(user)).Returns(Task.FromResult("token"));

            var urlHelper = A.Fake<IUrlHelper>();
            _controller.Url = urlHelper;
            A.CallTo(() => urlHelper.Action(A<UrlActionContext>.Ignored)).Returns("http://example.com/confirmation");

            // Act
            var result = await _controller.SignUp(signUpDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var response = okResult.Value as ErrorResponse;
            response.Should().NotBeNull();
            response.Message.Should().Be("User created and Email Sent to newuser@example.com Successfully");
        }*/
    }
}
