using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Valuations
{
    public class ValuationLightResponse
    {
        public Confidence Confidence { get; set; }
        public Currency Currency { get; set; }
        public ValueRange ValueRange { get; set; }
    }
}