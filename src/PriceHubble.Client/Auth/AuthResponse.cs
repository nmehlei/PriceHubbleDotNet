namespace PriceHubble.Client.Auth
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
    }
}