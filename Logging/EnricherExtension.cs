using Serilog;
using Serilog.Configuration;

namespace Logging
{
    public static class EnricherExtension
    {
        public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration) 
            => enrichmentConfiguration.With<CallerEnricher>();
    }
}
