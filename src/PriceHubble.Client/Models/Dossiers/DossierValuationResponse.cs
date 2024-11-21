using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Models.Dossiers
{
    public class DossierValuationResponse
    {
        /// <summary>
        /// Estimated sale price value of the property
        /// Only present if dealType is sale or is unset
        /// </summary>
        public Valuation? ValuationSale { get; set; }

        /// <summary>
        /// Estimated sale price timeline of the property
        /// Only present if dealType is sale or is unset
        /// </summary>
        public Valuation[]? ValuationSaleTimeline { get; set; }

        /// <summary>
        /// Estimated monthly net rent value of the property
        /// Only present if dealType is rent or is unset,
        /// and if net rent valuations are supported in the current country
        /// </summary>
        public Valuation? ValuationRentNet { get; set; }

        /// <summary>
        /// Estimated monthly net rent timeline of the property
        /// Only present if dealType is rent or is unset,
        /// and if net rent valuations are supported in the current country
        /// </summary>
        public Valuation[]? ValuationRentNetTimeline { get; set; }

        /// <summary>
        /// Estimated monthly gross rent value of the property
        /// Only present if dealType is rent or is unset,
        /// and if gross rent valuations are supported in the current country
        /// </summary>
        public Valuation? ValuationRentGross { get; set; }

        /// <summary>
        /// Estimated monthly gross rent timeline of the property
        /// Only present if dealType is rent or is unset,
        /// and if gross rent valuations are supported in the current country
        /// </summary>
        public Valuation[]? ValuationRentGrossTimeline { get; set; }

        /// <summary>
        /// Indicates whether the valuation is still relevant
        /// Always true when the result is seen from the current dossiers/<dossier_id>/valuation endpoint
        /// </summary>
        public bool? IsValuationStale { get; set; }
    }
}