using System.Net;
using PriceHubble.Client.Auth;
using PriceHubble.Client.Options;
using System.Text.Json;
using System.Text;
using PriceHubble.Client.Valuations;

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
        protected const string AuthorizationHeaderName = "Authorization";

        private string? _accessToken = null;
        private DateTime? _accessTokenExpiryDate;

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

        private bool IsAccessTokenCachedAndValid()
        {
            return _accessToken != null
                && _accessTokenExpiryDate != null
                && DateTime.UtcNow < _accessTokenExpiryDate.Value;
        }

        private AuthRequest GenerateAuthRequest()
        {
            if(string.IsNullOrWhiteSpace(_options.Username) || string.IsNullOrWhiteSpace(_options.Password))
                throw new InvalidOperationException("Credentials were not set in options");

            return new AuthRequest()
            {
                Username = _options.Username,
                Password = _options.Password,
            };
        }

        private void UpdateCachedAuthToken(string token, int expiryOffset)
        {
            _accessToken = token;
            //TODO: move -5 minutes to constant
            // NOTE: reduce by 5 minutes to minimize risk of still using an expired token,
            // e.g. due to clock mismatch or processing time
            _accessTokenExpiryDate = DateTime.UtcNow.AddSeconds(expiryOffset).AddMinutes(-5);
        }

        private async Task<string> GetAccessToken()
        {
            if(!IsAccessTokenCachedAndValid())
            {
                var authResult = await AuthAsync(GenerateAuthRequest());
                UpdateCachedAuthToken(authResult.AccessToken, authResult.ExpiresIn.Value);
            }

            return _accessToken;
        }

        private async Task AttachAccessToken(StringContent content)
        {
            var accessToken = await GetAccessToken();
            var header = string.Format("Bearer {0}", accessToken);
            content.Headers.Add(AuthorizationHeaderName, header);
        }

        public async Task<AuthResponse> AuthAsync(AuthRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, UsedMediaType);
            var responseRaw = await _httpClient.PostAsync("/auth/login/credentials", stringContent);

            if (!responseRaw.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var responseJson = await responseRaw.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<AuthResponse>(responseJson);
            return response;
        }

        public async Task<ValuationResponse> ValuationAsync(ValuationRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, UsedMediaType);
            await AttachAccessToken(stringContent);
            var responseRaw = await _httpClient.PostAsync("/api/v1/valuation/property_value", stringContent);

            if (!responseRaw.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var responseJson = await responseRaw.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ValuationResponse>(responseJson);
            return response;
        }
    }
}