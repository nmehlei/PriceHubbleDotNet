namespace PriceHubble.Client.Models.Dossiers
{
    public class DossierSharingResponse
    {
        /// <summary>
        /// Unique ID of the generated link
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Shareable link to PriceHubble Dash.
        /// </summary>
        public string Url { get; set; }
    }
}