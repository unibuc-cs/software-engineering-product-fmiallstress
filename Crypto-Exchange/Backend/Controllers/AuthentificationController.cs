using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Service.UserService;
using test_binance_api.Service.UserWalletHistoryService;
using test_binance_api.Models;
using Mailing.Service.Models;
using Mailing.Service.Services;
using test_binance_api.Models.Errors;
using System.Diagnostics.Eventing.Reader;
using AutoMapper;

namespace test_binance_api.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AuthentificationController(UserManager<User> userManager, IUserService userService,
                                            IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO signup)
        {
            try
            {
                var existsUser = await _userManager.FindByEmailAsync(signup.Email);

                if (existsUser != null)
                    throw new Exception("Email is already used");

                var user = _mapper.Map<User>(signup);
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Regular");

                    // Create auth token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var configurationLink = Url.Action(nameof(ConfirmEmail), "Authentification", new { token, email = user.Email });

                    var messageBody = $"Use the following token to confirm your email: {token}. Visit our confirmation page to complete the process.";
                    var message = new Message(new String[] { user.Email! }, "Email Confirmation Token", messageBody);
                    _emailService.SendEmail(message);

                    // Send verification url

                    _emailService.SendEmail(message);

                    return Ok(new ErrorResponse()
                    {
                        StatusCode = 200,
                        Message = $"User created and Email Sent to {user.Email} Successfully"
                    });
                }
                throw new Exception(result.Errors.First().Description);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            try
            {
                return Ok(await _userService.Login(user));

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            try
            {
                await _userService.Logout();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("isLogged")]
        public bool isLogged()
        {
            if (User.Identity.IsAuthenticated)
                return true;

            return false;

        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 500,
                    Message = "User Does Not Exist"
                });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new ErrorResponse()
                {
                    StatusCode = 200,
                    Message = "Email Verified Successfully"
                });
            }

            return BadRequest(new ErrorResponse()
            {
                StatusCode = 500,
                Message = "Error occured"
            }); ;

        }


        [HttpGet("isemailconfirmed")]
        public async Task<IActionResult> IsEmailConfirmed([FromQuery] string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound(new ErrorResponse()
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    });
                }

                var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                return Ok(new { Email = email, IsEmailConfirmed = isEmailConfirmed });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
