using System.Net;
using PriceHubble.Client.Auth;
using PriceHubble.Client.Options;
using System.Text.Json;
using System.Text;
using PriceHubble.Client.Valuation;

namespace PriceHubble.Client
{
    public class PriceHubbleClient
    {
        public PriceHubbleClient(PriceHubbleOptions options, TimeSpan? defaultRequestTimeout = null)
        {
            _options = options;

            _defaultRequestTimeout = defaultRequestTimeout ?? TimeSpan.FromSeconds(10);

            InitializeClient();
        }

        private readonly PriceHubbleOptions _options;
        private HttpClient _httpClient;
        private readonly TimeSpan _defaultRequestTimeout;

        protected const string UsedMediaType = "application/json";

        private void InitializeClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = _options.BaseAddress,
                Timeout = _defaultRequestTimeout
            };
        }

        public async Task<AuthResponse> AuthAsync(AuthRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, UsedMediaType);
            var responseRaw = await _httpClient.PostAsync("API/v1/Dispatch/DispatchMail", stringContent).ConfigureAwait(false);

            if (!responseRaw.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var responseJson = await responseRaw.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonSerializer.Deserialize<AuthResponse>(responseJson);
            return response;
        }

        public async Task<ValuationResponse> ValuationAsync(ValuationRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, UsedMediaType);
            var responseRaw = await _httpClient.PostAsync("API/v1/Dispatch/DispatchMail", stringContent).ConfigureAwait(false);

            if (!responseRaw.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var responseJson = await responseRaw.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonSerializer.Deserialize<ValuationResponse>(responseJson);
            return response;
        }
    }
}