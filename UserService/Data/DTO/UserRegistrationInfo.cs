namespace UserService.Data.DTO
{
    public class UserRegistrationInfo
    {
        public required string Login { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}