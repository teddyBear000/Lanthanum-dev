using Serilog;
using Serilog.Configuration;

namespace LoggingLibriary.Extensions
{
    public static class EnricherExtension
    {
        public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
            => enrichmentConfiguration.With<CallerEnricher>();
    }
}
