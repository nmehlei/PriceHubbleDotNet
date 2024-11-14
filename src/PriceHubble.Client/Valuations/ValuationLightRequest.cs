using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Valuations
{
    public class ValuationLightRequest
    {
        public DealType DealType { get; set; }
        public PropertyLight Property { get; set; }
        public string? DossierId { get; set; }
        public string CountryCode { get; set; }
    }
}