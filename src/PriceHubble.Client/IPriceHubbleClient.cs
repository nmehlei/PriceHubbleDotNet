using PriceHubble.Client.Auth;
using PriceHubble.Client.Results;
using PriceHubble.Client.Valuations;

namespace PriceHubble.Client
{
    public interface IPriceHubbleClient
    {
        Task<AuthResponse> AuthAsync(AuthRequest request, CancellationToken? cancellationToken = null);
        Task<ServiceResult<ValuationResponse>> ValuationAsync(ValuationRequest request, CancellationToken? cancellationToken = null);
        Task<ServiceResult<ValuationLightResponse>> ValuationLightAsync(ValuationLightRequest request, CancellationToken? cancellationToken = null);
    }
}