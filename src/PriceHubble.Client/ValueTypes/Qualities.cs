namespace PriceHubble.Client.ValueTypes
{
    public class Qualities
    {
        /// <summary>
        /// Bathrooms quality
        /// </summary>
        public Quality? Bathrooms { get; set; }

        /// <summary>
        /// Kitchen quality
        /// </summary>
        public Quality? Kitchen { get; set; }

        /// <summary>
        /// Flooring quality
        /// </summary>
        public Quality? Flooring { get; set; }

        /// <summary>
        /// Windows quality
        /// </summary>
        public Quality? Windows { get; set; }

        /// <summary>
        /// Masonry quality
        /// Only for house
        /// </summary>
        public Quality? Masonry { get; set; }
    }
}