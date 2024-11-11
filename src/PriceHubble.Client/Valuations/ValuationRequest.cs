using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Valuations
{
    public class ValuationRequest
    {
        public DealType DealType { get; set; }
        public DateTime[]? ValuationDates { get; set; }
        public List<ValuationInput> ValuationInputs { get; set;}
        public bool ReturnScores { get; set; } = true;
        public string? DossierId { get; set; }
        public string CountryCode { get; set;}
    }
}