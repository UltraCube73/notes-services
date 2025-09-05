namespace AuthService.Data.DTO
{
    public class UserRegistrationInfo
    {
        public required string Nickname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}