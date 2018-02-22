using bilisimHR.Common.Core;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;

namespace bilisimHR.Common.Logger
{
    public class NLogLogger : ILoggerService
    {
        private static readonly object _lock = new object();
        private static readonly Dictionary<Type, ILogger> _loggers = new Dictionary<Type, ILogger>();
        private static string _configurationFile = string.Empty;
        private static LoggingConfiguration _loggingConfiguration;

        public NLogLogger(string configurationFile)
        {
            _configurationFile = configurationFile;
            _loggingConfiguration = new XmlLoggingConfiguration(_configurationFile, true);
        }

        private static ILogger getLogger(Type source)
        {
            lock (_lock)
            {
                LogManager.Configuration = _loggingConfiguration;
                if (_loggers.ContainsKey(source))
                    return _loggers[source];
                else
                {
                    
                    //ILogger logger = LogManager.GetLogger(_loggerName, source);
                    //ILogger logger = LogManager.GetCurrentClassLogger(source);
                    //ILogger logger = LogManager.GetLogger(source.Name);
                    //Yukarıdaki üç seçenek için parametre olarak geçilecek türün statik olması gerekmektedir.
                    ILogger logger = LogManager.GetCurrentClassLogger();

                    _loggers.Add(source, logger);
                    return logger;
                }
            }
        }

        public void Trace(Type source, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsTraceEnabled)
                return;

            var logEvent = getLogEvent(source, NLog.LogLevel.Trace, exception, message, args);
            logger.Log(logEvent);
        }

        public void Debug(Type source, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsDebugEnabled)
                return;

            var logEvent = getLogEvent(source, NLog.LogLevel.Debug, exception, message, args);
            logger.Log(logEvent);
        }

        public void Info(Type source, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsInfoEnabled)
                return;

            var logEvent = getLogEvent(source, NLog.LogLevel.Info, exception, message, args);
            logger.Log(logEvent);
        }

        public void Warning(Type source, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsWarnEnabled)
                return;

            var logEvent = getLogEvent(source, NLog.LogLevel.Warn, exception, message, args);
            logger.Log(logEvent);
        }

        public void Error(Type source, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsErrorEnabled)
                return;

            var logEvent = getLogEvent(source, NLog.LogLevel.Error, exception, message, args);
            logger.Log(logEvent);
        }

        public void Fatal(Type source, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsFatalEnabled)
                return;

            var logEvent = getLogEvent(source, NLog.LogLevel.Fatal, exception, message, args);
            logger.Log(logEvent);
        }

        public void Log(Type source, Core.LogLevel level, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            
            var logEvent = getLogEvent(source, level, exception, message, args);
            logger.Log(logEvent);
        }

        private LogEventInfo getLogEvent(Type source, Core.LogLevel level, Exception exception, string format, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo(NLog.LogLevel.FromOrdinal((int)level), source.FullName, string.Format(format, args));
            logEvent.Exception = exception;

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite.DeclaringType.FullName;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }
            else
            {
                assemblyProp = source.FullName;
                classProp = source.Name;
            }

            logEvent.Properties["LOGGER"] = assemblyProp;
            logEvent.Properties["CLASS"] = classProp;
            logEvent.Properties["METHOD"] = methodProp;
            logEvent.Properties["EXCEPTION"] = messageProp;
            logEvent.Properties["STACKTRACE"] = innerMessageProp;

            return logEvent;
        }

        private LogEventInfo getLogEvent(Type source, NLog.LogLevel level, Exception exception, string message, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo(level, source.FullName, string.Format(message, args));

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite.DeclaringType.FullName;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }
            else
            {
                assemblyProp = source.FullName;
                classProp = source.Name;
            }

            logEvent.Properties["LOGGER"] = assemblyProp;
            logEvent.Properties["CLASS"] = classProp;
            logEvent.Properties["METHOD"] = methodProp;
            logEvent.Properties["EXCEPTION"] = messageProp;
            logEvent.Properties["STACKTRACE"] = innerMessageProp;

            return logEvent;
        }
    }
}
