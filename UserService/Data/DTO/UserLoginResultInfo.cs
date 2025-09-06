namespace UserService.Data.DTO
{
    public class UserLoginResultInfo()
    {
        public bool IsSuccessful { get; set; }
        public string? Token { get; set; }
    }
}