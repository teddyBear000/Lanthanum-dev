using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Linq;
using Serilog.Configuration;
using Serilog.Core;


namespace Logging
{
    public class Logger : ILogger
    {
        private Serilog.Core.Logger _logger;
        private const string Template = "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz]} [{Level:u3}] [Thread {ThreadId}] {Message:lj} [{SourceContext:l}.{Caller}]{NewLine}";
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
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Warning, outputTemplate: Template)
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(path, restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: Template)
                .CreateLogger();
        }
    }
}
