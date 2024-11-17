using Mailing.Service.Models;
using Mailing.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using test_binance_api.Models;
using test_binance_api.Models.DTOs;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Models.Errors;
using test_binance_api.Service.UserService;


namespace test_binance_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(UserManager<User> userManager, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
        }



        [HttpPatch("device-token/{deviceToken}")]
        public async Task<IActionResult> StoreDeviceToken(string deviceToken)
        {
            string userId = _userManager.GetUserId(User);

            try
            {
                await _userService.StoreDeviceToken(userId, deviceToken);
            }
            catch (Exception exception)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 500,
                    Message = exception.Message
                });
            }
            return Ok(new ErrorResponse()
            {
                StatusCode = 200,
                Message = "Device token stored"
            });
        }


        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _userService.GetUserById(id));
            }
            catch(Exception exception)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 500,
                    Message = exception.Message
                });
            }
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsersAsync());
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync(UserCreateDTO user)
        {
            //TODO: to create a user as an admin
            try
            {
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateDTO user)
        {
            try
            {
                return Ok(await _userService.Update(user));
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                await _userService.Delete(id);
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

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail(string email, MessageDTO msgDTO)
        {
            try
            {
             var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        StatusCode = 500,
                        Message = "User doesnt exist"
                    });
                }

                var message = new Message(new string[] { email }, msgDTO.Subject, msgDTO.Content);

                _emailService.SendEmail(message);
                return Ok(new ErrorResponse()
                {
                    StatusCode = 200,
                    Message = "Email sent successfully"
                });

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
