using bilisimHR.Common.Core;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using NLogLogger = NLog;

namespace bilisimHR.Infrastructure.Logger.NLog
{
    public class CoreLogger : ICoreLogger
    {
        private static readonly object _lock = new object();
        private static readonly Dictionary<Type, ILogger> _loggers = new Dictionary<Type, ILogger>();
        private static LoggingConfiguration _loggingConfiguration;
        
        public CoreLogger()
        {
            string confFileTrace = System.Configuration.ConfigurationManager.AppSettings["LoggerConfigFile"] == null ? string.Empty : System.Configuration.ConfigurationManager.AppSettings["LoggerConfigFile"].ToString();
            string assemblyFolderTrace = AppDomain.CurrentDomain.RelativeSearchPath + @"\" + confFileTrace;
            _loggingConfiguration = new XmlLoggingConfiguration(assemblyFolderTrace, true);
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

        public void Trace(Type type, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsTraceEnabled)
                return;

            var logEvent = getLogEvent(type, Common.Core.LogLevel.Trace, exception, message, args);
            logger.Log(logEvent);
        }

        public void Debug(Type type, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsDebugEnabled)
                return;

            var logEvent = getLogEvent(type, Common.Core.LogLevel.Debug, exception, message, args);
            logger.Log(logEvent);
        }

        public void Info(Type type, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsInfoEnabled)
                return;

            var logEvent = getLogEvent(type, Common.Core.LogLevel.Info, exception, message, args);
            logger.Log(logEvent);
        }

        public void Warning(Type type, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsWarnEnabled)
                return;

            var logEvent = getLogEvent(type, Common.Core.LogLevel.Warning, exception, message, args);
            logger.Log(logEvent);
        }

        public void Error(Type type, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsErrorEnabled)
                return;

            var logEvent = getLogEvent(type, Common.Core.LogLevel.Error, exception, message, args);
            logger.Log(logEvent);
        }

        public void Fatal(Type type, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());
            if (!logger.IsFatalEnabled)
                return;

            var logEvent = getLogEvent(type, Common.Core.LogLevel.Fatal, exception, message, args);
            logger.Log(logEvent);
        }

        public void Log(Type type, Common.Core.LogLevel level, Exception exception, string message, params object[] args)
        {
            ILogger logger = getLogger(this.GetType());

            var logEvent = getLogEvent(type, level, exception, message, args);
            logger.Log(logEvent);
        }

        private LogEventInfo getLogEvent(Type type, Common.Core.LogLevel level, Exception exception, string format, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo(NLogLogger.LogLevel.FromOrdinal((int)level), type.FullName, string.Format(format, args));
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
                assemblyProp = type.FullName;
                classProp = type.Name;
            }

            if (logEvent.Properties.Count > 0)
            {
                logEvent.Properties["LOGGER"] = assemblyProp;
                logEvent.Properties["CLASS"] = classProp;
                logEvent.Properties["METHOD"] = methodProp;
                logEvent.Properties["EXCEPTION"] = messageProp;
                logEvent.Properties["STACKTRACE"] = innerMessageProp;
            }

            return logEvent;
        }

        private LogEventInfo getLogEvent(Type type, NLogLogger.LogLevel level, Exception exception, string message, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo(level, type.FullName, string.Format(message, args));

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
                assemblyProp = type.FullName;
                classProp = type.Name;
            }

            if (logEvent.Properties.Count > 0)
            {
                logEvent.Properties["LOGGER"] = assemblyProp;
                logEvent.Properties["CLASS"] = classProp;
                logEvent.Properties["METHOD"] = methodProp;
                logEvent.Properties["EXCEPTION"] = messageProp;
                logEvent.Properties["STACKTRACE"] = innerMessageProp;
            }

            return logEvent;
        }
    }
}
