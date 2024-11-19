using System.Net;
using PriceHubble.Client.Auth;
using PriceHubble.Client.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using PriceHubble.Client.Valuations;
using Microsoft.Extensions.Logging;
using System.Threading;
using PriceHubble.Client.Results;
using System.Diagnostics;
using System.Reflection;

namespace PriceHubble.Client
{
    public class PriceHubbleClient : IPriceHubbleClient
    {
        public PriceHubbleClient(ILogger<PriceHubbleClient> logger, PriceHubbleOptions options, TimeSpan? defaultRequestTimeout = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options;

            _defaultRequestTimeout = defaultRequestTimeout ?? TimeSpan.FromSeconds(10);

            logger.LogDebug("Test");

            InitializeClient();
        }

        private ILogger<PriceHubbleClient> _logger;

        private readonly PriceHubbleOptions _options;
        private HttpClient _httpClient;
        private readonly TimeSpan _defaultRequestTimeout;

        protected const string UsedMediaType = "application/json";
        protected const string AuthorizationHeaderName = "Authorization";

        private string? _accessToken = null;
        private DateTime? _accessTokenExpiryDate;

        private static readonly Uri DefaultBaseAddress = new Uri("https://api.pricehubble.com", UriKind.Absolute);

        private static ActivitySource ActivitySource = InitializeActivitySource();

        private static ActivitySource InitializeActivitySource()
        {
            var assembly = Assembly.GetAssembly(typeof(PriceHubbleClient));
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var fileVersion = fileVersionInfo.FileVersion;

            return new ActivitySource(
                name: "PriceHubble.Client.PriceHubbleClient",
                version: fileVersion
            );
        }

        private JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
            }
        };

        private void InitializeClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = _options.BaseAddress ?? DefaultBaseAddress,
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
            if (string.IsNullOrWhiteSpace(_options.Username) || string.IsNullOrWhiteSpace(_options.Password))
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

            _logger.LogDebug("Updated cached auth token");
        }

        private async Task<string> GetAccessToken()
        {
            if (!IsAccessTokenCachedAndValid())
            {
                _logger.LogDebug("Cached auth token missing or expired, requesting new token");
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

        public async Task<AuthResponse> AuthAsync(AuthRequest request, CancellationToken? cancellationToken = null)
        {
            using (var activity = ActivitySource.StartActivity("PriceHubble.Auth", ActivityKind.Client))
            {
                _logger.LogDebug("Invoking Auth");

                // NOTE: For some unknown reason, the formatting for the auth endpoint is snake_case whereas for the other endpoints it is camelCase
                var authSerializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    WriteIndented = false
                };

                var jsonBody = JsonSerializer.Serialize(request, authSerializeOptions);

                _logger.LogDebug("Invoking AuthAsync with body {Body}", jsonBody);

                var uri = "/auth/login/credentials";
                activity?.SetTag("uri", uri);

                var stringContent = new StringContent(jsonBody, Encoding.UTF8, UsedMediaType);
                var responseRaw = await _httpClient.PostAsync(uri, stringContent);
                activity?.SetTag("http.response_code", responseRaw.StatusCode.ToString());

                if (!responseRaw.IsSuccessStatusCode)
                {
                    activity?.SetStatus(ActivityStatusCode.Error);
                    throw new Exception();
                }

                activity?.SetStatus(ActivityStatusCode.Ok);

                var responseJson = await responseRaw.Content.ReadAsStringAsync();

                var response = JsonSerializer.Deserialize<AuthResponse>(responseJson, authSerializeOptions);
                return response;
            }
        }

        public async Task<ServiceResult<ValuationResponse>> ValuationAsync(ValuationRequest request, CancellationToken? cancellationToken = null)
        {
            using (var activity = ActivitySource.StartActivity("PriceHubble.Valuation", ActivityKind.Client))
            {
                var jsonBody = JsonSerializer.Serialize(request, _serializeOptions);
                const string uri = "/api/v1/valuation/property_value";
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

                activity?.SetTag("uri", uri);

                _logger.LogDebug("Invoking Valuation with body {Body}", jsonBody);

                var stringContent = new StringContent(jsonBody, Encoding.UTF8, UsedMediaType);
                httpRequest.Content = stringContent;
                httpRequest.Headers.Add("Authorization", string.Format("Bearer {0}", await GetAccessToken()));

                var responseRaw = await _httpClient.SendAsync(httpRequest);
                activity?.SetTag("http.response_code", responseRaw.StatusCode.ToString());

                if (!responseRaw.IsSuccessStatusCode)
                {
                    activity?.SetStatus(ActivityStatusCode.Error);
                    var errorResult = await responseRaw.Content.ReadAsStringAsync();
                    _logger.LogError("Error during ValuationAsync: {ErrorDetails}", errorResult);
                    return ServiceResult<ValuationResponse>.WithServerError(new ServerError(responseRaw.StatusCode.ToString(), errorResult));
                }

                activity?.SetStatus(ActivityStatusCode.Ok);

                var responseJson = await responseRaw.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<ValuationResponse>(responseJson, _serializeOptions);
                return response;
            }
        }

        public async Task<ServiceResult<ValuationLightResponse>> ValuationLightAsync(ValuationLightRequest request, CancellationToken? cancellationToken = null)
        {
            using (var activity = ActivitySource.StartActivity("PriceHubble.ValuationLight", ActivityKind.Client))
            {
                var jsonBody = JsonSerializer.Serialize(request, _serializeOptions);
                const string uri = "/api/v1/valuation/property_value_light";
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri);

                activity?.SetTag("uri", uri);

                _logger.LogDebug("Invoking ValuationLight with body {Body}", jsonBody);

                var stringContent = new StringContent(jsonBody, Encoding.UTF8, UsedMediaType);
                httpRequest.Content = stringContent;
                httpRequest.Headers.Add("Authorization", string.Format("Bearer {0}", await GetAccessToken()));

                var responseRaw = await _httpClient.SendAsync(httpRequest);
                activity?.SetTag("http.response_code", responseRaw.StatusCode.ToString());

                if (!responseRaw.IsSuccessStatusCode)
                {
                    activity?.SetStatus(ActivityStatusCode.Error);
                    var errorResult = await responseRaw.Content.ReadAsStringAsync();
                    _logger.LogError("Error during ValuationLightAsync: {ErrorDetails}", errorResult);
                    return ServiceResult<ValuationLightResponse>.WithServerError(new ServerError(responseRaw.StatusCode.ToString(), errorResult));
                }

                activity?.SetStatus(ActivityStatusCode.Ok);

                var responseJson = await responseRaw.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<ValuationLightResponse>(responseJson, _serializeOptions);
                return response;
            }
        }
    }
}