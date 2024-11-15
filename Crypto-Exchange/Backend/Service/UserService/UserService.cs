using AutoMapper;
using Mailing.Service.Models;
using Mailing.Service.Services;
using Microsoft.AspNetCore.Identity;
//using NETCore.MailKit.Core;
using System;
using test_binance_api.Models;
using test_binance_api.Models.DTOs.User;
using test_binance_api.Models.Errors;
using test_binance_api.Repository.UserRepository;


namespace test_binance_api.Service.UserService
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //will be used for smtp 
        private readonly IConfiguration _configuration;
        //for verif token
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        //create
        public async Task<UserDTO> CreateAsync(UserCreateDTO user)
        {
            var newUser = _mapper.Map<User>(user);
            Console.WriteLine(newUser.PasswordHash);
            await _userRepository.CreateAsync(newUser);
            return _mapper.Map<UserDTO>(newUser);
        }


        //find
        public async Task<UserDTO> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) throw new Exception("Invalid user");
            return _mapper.Map<UserDTO>(user);
        }


        //get all
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null) throw new Exception("No users found");
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task Delete(Guid id)
        {
            await _userRepository.Delete(id);
        }


        //update
        public async Task<UserDTO> Update(UserUpdateDTO user)
        {
            // Validate the user DTO
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.Id == Guid.Empty) throw new ArgumentException("User ID cannot be empty", nameof(user.Id));

            var existingUser = await _userRepository.GetUserById(user.Id);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            UpdateUserFields(existingUser, user);

            await _userRepository.Update(existingUser);
            return _mapper.Map<UserDTO>(existingUser);
        }
        
        private void UpdateUserFields(User existingUser, UserUpdateDTO user)
        {
            if (!string.IsNullOrWhiteSpace(user.UserName)) existingUser.UserName = user.UserName;
            if (!string.IsNullOrWhiteSpace(user.FirstName)) existingUser.FirstName = user.FirstName;
            if (!string.IsNullOrWhiteSpace(user.LastName)) existingUser.LastName = user.LastName;
            if (!string.IsNullOrWhiteSpace(user.Email)) existingUser.Email = user.Email;
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                var hasher = new PasswordHasher<User>();
                existingUser.PasswordHash = hasher.HashPassword(existingUser, user.Password);
            }
        }

        public async Task<Guid> Login(LoginDTO loginDto)
        {
            var email = await _userManager.FindByEmailAsync(loginDto.Email);
            if (email == null)
            {
                throw new Exception("The email does not exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(email, loginDto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(email, isPersistent: true);


                User user = await _userRepository.GetUserById(email.Id);
                user.Consent = true;
                await _userRepository.Update(user);

                return email.Id;
            }

            throw new Exception("Wrong password");
        }
        

/*        public Boolean IsLogged()
        {
            var user = _userRepository.GetUserById();
            var isLogged = _signInManager.IsSignedIn();

            return isLogged;
        }*/


        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task StoreDeviceToken(string IdUser, string deviceToken)
        {
            var user = await _userRepository.GetUserById(new Guid(IdUser));

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.DeviceToken = deviceToken;
            await _userRepository.Update(user);
        }


    }
}
