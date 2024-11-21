namespace PriceHubble.Client.Models.Dossiers
{
    public class DossierSharingRequest
    {
        public DossierSharingRequest(string dossierId, string countryCode, string locale)
        {
            DossierId = dossierId;
            CountryCode = countryCode;
            Locale = locale;
        }

        /// <summary>
        /// Unique ID of the dossier
        /// </summary>
        public string DossierId { get; set; }

        /// <summary>
        /// Number of days for the link to be valid
        /// min: 1, max: 365, default: 365
        /// </summary>
        public int? DaysToLive { get; set; }

        /// <summary>
        /// ISO country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// User locale
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Whether a PDF can be printed from the link
        /// </summary>
        public bool? CanGeneratePdf { get; set; }
    }
}