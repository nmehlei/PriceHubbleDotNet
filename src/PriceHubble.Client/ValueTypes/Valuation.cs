namespace PriceHubble.Client.ValueTypes
{
    public class Valuation
    {
        /// <summary>
        /// Estimated sale price / monthly net rent / monthly gross rent value of the property
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Estimated lower/upper bound of the property's sale price / net rent / gross rent value
        /// </summary>
        public ValueRange ValueRange { get; set; }

        /// <summary>
        /// The date of the valuation
        /// YYYY-MM-DD
        /// </summary>
        public DateTime ValuationDate { get; set; }

        /// <summary>
        /// Confidence of the valuation
        /// Valid values are poor, medium or good
        /// </summary>
        public Confidence ValuationConfidence { get; set; }
    }
}