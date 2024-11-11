using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Valuations
{
    public class ValuationResponse
    {
        /// <summary>
        /// List of valuations per each property per each date
        /// valuations[i][j] contains the valuation for property i on date j
        /// </summary>
        public Valuation[][] Valuations { get; set; }

        public Confidence Confidence { get; set; }
    }
}