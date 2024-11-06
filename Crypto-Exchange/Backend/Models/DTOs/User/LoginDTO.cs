using System.ComponentModel.DataAnnotations;

namespace test_binance_api.Models.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
