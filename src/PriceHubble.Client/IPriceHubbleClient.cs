using PriceHubble.Client.Auth;
using PriceHubble.Client.Valuation;

namespace PriceHubble.Client
{
    public interface IPriceHubbleClient
    {
        Task<AuthResponse> AuthAsync(AuthRequest request);
        Task<ValuationResponse> ValuationAsync(ValuationRequest request);
    }
}