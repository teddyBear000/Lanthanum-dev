using Serilog;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace Logging
{
    class CallerEnricher : ILogEventEnricher
    {
        private List<Assembly> _assemblies = new List<Assembly>() 
        {
            typeof(Log).Assembly, 
            typeof(Logger).Assembly 
        };
        private bool IsCalledFromLogger(Assembly assembly) => _assemblies.Contains(assembly);
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var skip = 3;
            while (true)
            {
                var stack = new StackFrame(skip);
                if (!stack.HasMethod())
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
                    return;
                }

                var method = stack.GetMethod();
                if (!IsCalledFromLogger(method.DeclaringType.Assembly))
                {
                    var caller = $"{method.DeclaringType.FullName}.{method.Name}({string.Join(", ", method.GetParameters().Select(pi => pi.ParameterType.FullName))})";
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(caller)));
                    return;
                }

                skip++;
            }
        }
    }
}
