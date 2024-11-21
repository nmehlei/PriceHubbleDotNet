using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Models.Valuations
{
    public class ValuationLightResponse
    {
        public Confidence Confidence { get; set; }
        public Currency Currency { get; set; }
        public ValueRange ValueRange { get; set; }
    }
}