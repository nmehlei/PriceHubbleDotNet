using PriceHubble.Client.ValueTypes;

namespace PriceHubble.Client.Models.Dossiers
{
    public class DossierCreationRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DealType DealType { get; set; }
        public Property Property { get; set; }

        /// <summary>
        /// ISO country code
        /// In France, sale valuations are supported for the following remote territories: Guadeloupe, Guyane, Martinique and Réunion; any other remote territories and rent valuations are currently not supported.
        /// </summary>
        public string CountryCode { get; set; }
    }
}