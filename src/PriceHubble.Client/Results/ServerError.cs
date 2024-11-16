namespace PriceHubble.Client.Results
{
    public class ServerError
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public ServerError(string errorCode, string errorDescription)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}