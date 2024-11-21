using PriceHubble.Client.Auth;
using PriceHubble.Client.Results;
using PriceHubble.Client.Models.Valuations;
using PriceHubble.Client.Models.Dossiers;

namespace PriceHubble.Client
{
    public interface IPriceHubbleClient
    {
        Task<AuthResponse> AuthAsync(AuthRequest request, CancellationToken? cancellationToken = null);

        Task<ServiceResult<ValuationResponse>> ValuationAsync(ValuationRequest request, CancellationToken? cancellationToken = null);
        Task<ServiceResult<ValuationLightResponse>> ValuationLightAsync(ValuationLightRequest request, CancellationToken? cancellationToken = null);

        Task<ServiceResult<DossierCreationResponse>> DossierCreationAsync(DossierCreationRequest request, CancellationToken? cancellationToken = null);
        Task<ServiceResult<DossierValuationResponse>> DossierValuationAsync(string dossierId, DossierValuationRequest request, CancellationToken? cancellationToken = null);
        Task<ServiceResult<DossierSharingResponse>> DossierSharingAsync(DossierSharingRequest request, CancellationToken? cancellationToken = null);
    }
}