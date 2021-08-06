using Serilog;
using Serilog.Enrichers;
using Serilog.Events;

namespace Logging
{
    public class Logger : ILogger
    {
        private Serilog.Core.Logger _logger;
        private const string Template = "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz]} [{Level:u3}] [Thread {ThreadId}] {Message:lj}{NewLine}";
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Info(string message)
        {
            _logger.Information(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);            
        }

        public Logger(string path)
        {
            _logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .MinimumLevel.Warning()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Warning, outputTemplate: Template)
                .MinimumLevel.Verbose()
                .WriteTo.File(path, restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: Template)
                .CreateLogger();
        }
    }
}
