using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.Valuation
{
    public class Valuation
    {
        /// <summary>
        /// Estimated sale price / monthly net rent / monthly gross rent value of the property
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// The date of the valuation
        /// </summary>
        public DateTime ValuationDate { get; set; }

        /// <summary>
        /// Confidence of the valuation
        /// </summary>
        public Confidence ValuationConfidence { get; set; }
    }
}