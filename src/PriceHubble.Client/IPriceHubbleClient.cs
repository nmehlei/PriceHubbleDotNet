using PriceHubble.Client.Auth;
using PriceHubble.Client.Valuations;
using System.Threading;

namespace PriceHubble.Client
{
    public interface IPriceHubbleClient
    {
        Task<AuthResponse> AuthAsync(AuthRequest request, CancellationToken? cancellationToken = null);
        Task<ValuationResponse> ValuationAsync(ValuationRequest request, CancellationToken? cancellationToken = null);
        Task<ValuationLightResponse> ValuationLightAsync(ValuationLightRequest request, CancellationToken? cancellationToken = null);
    }
}