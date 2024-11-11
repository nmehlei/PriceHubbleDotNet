namespace PriceHubble.Client.Options
{
    public class PriceHubbleOptions
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public Uri BaseAddress { get; set; } = new Uri("https://api.pricehubble.com", UriKind.Absolute);
    }
}