namespace UserService.Data.Model
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public required string Nickname { get; set; }
        public required string Email { get; set; }
        public required byte[] Password { get; set; }
        public required byte[] PasswordSalt { get; set; }
    }
}