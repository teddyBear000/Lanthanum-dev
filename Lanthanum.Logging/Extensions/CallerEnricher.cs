using Serilog;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace Lanthanum.Logging.Extensions
{
    class CallerEnricher : ILogEventEnricher
    {
        private List<Assembly> _assemblies = new List<Assembly>()
        {
            typeof(Log).Assembly,
            typeof(Logger).Assembly
        };

        private bool IsCalledFromLogger(MethodBase method) => _assemblies.Contains(method.DeclaringType.Assembly);

        private string GetCallerData(MethodBase method) => $"{method.DeclaringType.FullName}.{method.Name}({string.Join(", ", method.GetParameters().Select(pi => pi.ParameterType.FullName))})";

        private void UpdateValues(ref int index, ref StackFrame stack) => (index, stack) = (++index, new StackFrame(index));

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var skipFirstStackFrames = 3;
            for (var stack = new StackFrame(skipFirstStackFrames); stack.HasMethod(); UpdateValues(ref skipFirstStackFrames, ref stack))
            {
                var currentMethod = stack.GetMethod();
                if (IsCalledFromLogger(currentMethod)) continue;
                logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(GetCallerData(currentMethod))));
                return;
            }
            logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
        }
    }
}
