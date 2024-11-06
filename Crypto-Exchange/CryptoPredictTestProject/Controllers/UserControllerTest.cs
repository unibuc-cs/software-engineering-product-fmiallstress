using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Mailing.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_binance_api.Models;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Service.UserService;
using test_binance_api.Service.UserWalletHistoryService;
using Xunit;

namespace CryptoPredictTestProject.Controllers
{
    public class UserControllerTest
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IUserWalletHistoryService _userWalletHistoryService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly test_binance_api.Controllers.UserController _controller;

        public UserControllerTest()
        {
            _userManager = A.Fake<UserManager<User>>();
            _userService = A.Fake<IUserService>();
            _userWalletHistoryService = A.Fake<IUserWalletHistoryService>();
            _emailService = A.Fake<IEmailService>();
            _mapper = A.Fake<IMapper>();
            _controller = new test_binance_api.Controllers.UserController(_userManager, _userService, _userWalletHistoryService, _emailService);
        }

        [Fact]
        public async void UserController_StoreDeviceToken_ReturnOK()
        {
            //Arrange
            string deviceToken = "test_device_token";

            //Act
            var result = await _controller.StoreDeviceToken(deviceToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void UserController_GetById_ReturnOK()
        {
            //Arrange
            Guid id = new Guid("9268a6e5-3bdf-4afc-8647-3501653e9d55");

            //Act
            var result = await _controller.GetById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void UserController_GetAllUsers_ReturnOk()
        {
            //Arrange
            var users = A.Fake<ICollection<UserDTO>>();
            var userList = A.Fake<List<UserDTO>>();
            A.CallTo(() => _mapper.Map<List<UserDTO>>(users)).Returns(userList);

            //Act
            var result = await _controller.GetAllUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
