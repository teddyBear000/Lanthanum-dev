using Serilog;
using Serilog.Configuration;

namespace Lanthanum.Logging.Extensions
{
    public static class EnricherExtension
    {
        public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
            => enrichmentConfiguration.With<CallerEnricher>();
    }
}
