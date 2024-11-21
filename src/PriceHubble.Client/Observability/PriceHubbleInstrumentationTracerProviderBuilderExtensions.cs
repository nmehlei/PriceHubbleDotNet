using OpenTelemetry.Trace;

namespace PriceHubble.Client.Observability
{
    public static class PriceHubbleInstrumentationTracerProviderBuilderExtensions
    {
        public static TracerProviderBuilder AddPriceHubbleInstrumentation(this TracerProviderBuilder builder)
        {
            return builder.AddSource(PriceHubbleObservabilityConstants.ActivitySourceName);
        }
    }
}