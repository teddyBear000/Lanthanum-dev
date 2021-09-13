﻿using Serilog;
using System;
using System.IO;
using Serilog.Events;
using Lanthanum.Logging.Extensions;

namespace Lanthanum.Logging
{
    public class Logger : ILogger
    {
        private Serilog.Core.Logger _logger;

        private const string Template = "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz]} [{Level:u3}] [Thread {ThreadId}] {Message:lj} (at {Caller}){NewLine}";

        private string GetPath(string path) => path == string.Empty ? Path.Combine(Environment.CurrentDirectory, "logger.log") : path;

        public void Debug(string message) => _logger.Debug(message);

        public void Error(string message) => _logger.Error(message);

        public void Fatal(string message) => _logger.Fatal(message);

        public void Info(string message) => _logger.Information(message);

        public void Warning(string message) => _logger.Warning(message);

        public Logger(string path = "") => _logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .Enrich.WithCaller()
                .MinimumLevel.Warning()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Warning, outputTemplate: Template)
                .MinimumLevel.Verbose()
                .WriteTo.File(GetPath(path), restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: Template)
                .CreateLogger();
    }
}