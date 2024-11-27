﻿using System.Collections.Concurrent;

namespace Catalogo.Logging
{
    public class CustomerLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration _loggerConfig;
        readonly ConcurrentDictionary<string,CustomerLogger> _loggers = new ConcurrentDictionary<string,CustomerLogger>();

        public CustomerLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            _loggerConfig = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, _loggerConfig));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
