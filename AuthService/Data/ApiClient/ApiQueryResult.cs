namespace AuthService.Data
{
    public class ApiQueryResult
    {
        public ApiQueryResult(bool isSuccessful, string status)
        {
            this.isSuccessful = isSuccessful;
            this.status = status;
        }

        public readonly bool isSuccessful;
        public readonly string status;
    }
}