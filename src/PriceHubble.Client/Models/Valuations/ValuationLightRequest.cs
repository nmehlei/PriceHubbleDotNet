using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Models.Valuations
{
    public class ValuationLightRequest
    {
        public DealType DealType { get; set; }
        public PropertyLight Property { get; set; }
        public string? DossierId { get; set; }
        public string CountryCode { get; set; }
    }
}