namespace test_binance_api.Models.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? DeviceToken { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public decimal? Balance { get; set; }
        public bool? Consent { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
