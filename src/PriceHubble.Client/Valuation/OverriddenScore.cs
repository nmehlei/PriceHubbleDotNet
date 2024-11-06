using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceHubble.Client.Valuation
{
    public class OverriddenScore
    {
        /// <summary>
        /// Score was overridden
        /// </summary>
        public bool? IsOverridden { get; set; }

        /// <summary>
        /// Original score value
        /// min: 0.0 (very bad)
        /// max: 1.0 (very good)
        /// </summary>
        public decimal? OriginalScore { get; set; }

        /// <summary>
        /// Current score value
        /// min: 0.0 (very bad)
        /// max: 1.0 (very good)
        /// If isOverridden is true this will be overridden value, otherwise it will be originalScore
        /// </summary>
        public decimal? Score { get; set; }
    }
}