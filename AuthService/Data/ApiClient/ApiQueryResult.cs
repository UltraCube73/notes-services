namespace AuthService.Data
{
    public class ApiQueryResult
    {
        public ApiQueryResult(bool isSuccessful)
        {
            this.isSuccessful = isSuccessful;
        }

        public readonly bool isSuccessful;
    }
}