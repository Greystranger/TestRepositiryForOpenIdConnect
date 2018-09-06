using System;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TestASPNetCoreApp.Logger
{
    public class Logger : ILogger
    {
        private static readonly NLogLoggerFactory loggerFactory;
        private readonly ILogger m_Logger;

        static Logger()
        {
            loggerFactory = new NLogLoggerFactory();
        }

        private Logger(ILogger logger)
        {
            m_Logger = logger;
        }

        public static ILogger GetCurrentClassLogger(Type type)
        {
            var logger = loggerFactory.CreateLogger(type);
            return new Logger(logger);
        }


        public static ILogger GetCurrentClassLogger<T>()
        {
            return GetCurrentClassLogger(typeof(T));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return m_Logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return m_Logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            m_Logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
