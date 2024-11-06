using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.Valuation
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