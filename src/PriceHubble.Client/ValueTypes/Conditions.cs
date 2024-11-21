namespace PriceHubble.Client.ValueTypes
{
    public class Conditions
    {
        /// <summary>
        /// Bathrooms condition
        /// </summary>
        public Condition? Bathrooms { get; set; }

        /// <summary>
        /// Kitchen condition
        /// </summary>
        public Condition? Kitchen { get; set; }

        /// <summary>
        /// Flooring condition
        /// </summary>
        public Condition? Flooring { get; set; }

        /// <summary>
        /// Windows condition
        /// </summary>
        public Condition? Windows { get; set; }

        /// <summary>
        /// Masonry condition
        /// Only for house
        /// </summary>
        public Condition? Masonry { get; set; }
    }
}