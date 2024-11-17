using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Service.UserService;
using test_binance_api.Service.UserWalletService;
using test_binance_api.Models;
using Mailing.Service.Models;
using Mailing.Service.Services;
using test_binance_api.Models.Errors;
using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using System.Web;
using System.Security.Claims;

namespace test_binance_api.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUserWalletService _userWalletService;

        public AuthentificationController(UserManager<User> userManager, IUserService userService,
                                            IEmailService emailService, IMapper mapper, IUserWalletService userWalletService)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
            _userWalletService = userWalletService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO signup)
        {
            try
            {
                var existsUser = await _userManager.FindByEmailAsync(signup.Email);
                if (existsUser != null)
                    throw new Exception("Email is already used");

                // Map the signup DTO to a User entity
                var user = _mapper.Map<User>(signup);

                // Persist the user and their wallet
                await _userWalletService.CreateUserWithWalletAsync(user);

                // Reload the user to ensure changes (like Id) are reflected
                var persistedUser = await _userManager.FindByEmailAsync(user.Email);
                if (persistedUser == null)
                    throw new Exception("Failed to retrieve the user after creation.");

                // Generate the email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(persistedUser);

                // Encode the token to ensure it’s URL-safe
                var encodedToken = HttpUtility.UrlEncode(token);

                // Generate the confirmation link
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentification", new { token = encodedToken, email = persistedUser.Email }, Request.Scheme);

                // Send the email
                var messageBody = $"Please confirm your email by clicking the link: {confirmationLink}";
                var message = new Message(new string[] { persistedUser.Email }, "Email Confirmation Token", messageBody);
                _emailService.SendEmail(message);

                return Ok(new ErrorResponse()
                {
                    StatusCode = 200,
                    Message = $"User and Wallet created. Email sent to {persistedUser.Email} successfully."
                });
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

        [HttpGet("whoIsLogged")]
        public IActionResult GetCurrentUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { Message = "User is not logged in." });
            }

            // Retrieve the user ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { Message = "Could not retrieve user ID." });
            }

            return Ok(new { UserId = userId });
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {

            var decodedToken = HttpUtility.UrlDecode(token);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = 500,
                    Message = "User Does Not Exist"
                });
            }

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                return Ok(new ErrorResponse()
                {
                    StatusCode = 200,
                    Message = "Email Verified Successfully"
                });
            }

            var errors = result.Errors.Select(e => e.Description).ToList();

            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Email verification failed",
                Errors = errors
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
