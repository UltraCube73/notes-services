namespace UserService.Data.DTO
{
    public class UserExistenceInfo
    {
        public required bool LoginExists { get; set; }
        public required bool EmailExists { get; set; }
    }
}