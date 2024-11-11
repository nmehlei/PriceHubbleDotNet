namespace PriceHubble.Client.ValueTypes
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