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
                Console.WriteLine("AM AJUNS AICI!02452");

                var user = _mapper.Map<User>(signup);
                Console.WriteLine("AM AJUNS AICI!0");

                var result = await _userManager.CreateAsync(user);
                Console.WriteLine("AM AJUNS AICI!");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Regular");
                    Console.WriteLine("AM AJUNS AICI!1");

                    // Create auth token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    Console.WriteLine("AM AJUNS AICI2!");

                    var configurationLink = Url.Action(nameof(ConfirmEmail), "Authentification", new { token, email = user.Email });
                    Console.WriteLine("AM AJUNS AICI3!");

                    var message = new Message(new String[] { user.Email! }, "Confirmation email link", configurationLink!);
                    Console.WriteLine("AM AJUNS AICI!4");

                    // Send verification url

                    // _emailService.SendEmail(message);

                    Console.WriteLine("AM AJUNS AICI5!");

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
            try {
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
    }
}
