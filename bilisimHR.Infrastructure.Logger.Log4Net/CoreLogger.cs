using bilisimHR.Common.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;

namespace bilisimHR.Infrastructure.Logger.Log4Net
{
    public class CoreLogger : ICoreLogger
    {
        private static readonly Dictionary<Type, ILog> _loggers = new Dictionary<Type, ILog>();
        private static readonly object _lock = new object();
        private static string _configurationFile;

        public CoreLogger()
        {
            string confFileGeneric = System.Configuration.ConfigurationManager.AppSettings["LoggerConfigFile"] == null ? string.Empty : System.Configuration.ConfigurationManager.AppSettings["LoggerConfigFile"].ToString();
            string assemblyFolderGeneric = AppDomain.CurrentDomain.RelativeSearchPath + @"\" + confFileGeneric;
            _configurationFile = assemblyFolderGeneric;
            //_configurationFile = configurationFile;
        }

        private static ILog getLogger(Type source)
        {
            lock (_lock)
            {
                if (_loggers.ContainsKey(source))
                    return _loggers[source];
                else
                {
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(_configurationFile));
                    ILog logger = LogManager.GetLogger(source);
                    _loggers.Add(source, logger);
                    return logger;
                }
            }
        }

        public void Trace(Type type, Exception exception = null, string message = "", params object[] args)
        {
            ILog logger = getLogger(type);
            if (logger.IsInfoEnabled)
            {
                setProperties(type, exception);
                logger.Info(string.Format(message, args), exception);
            }
        }

        public void Debug(Type type, Exception exception = null, string message = "", params object[] args)
        {
            ILog logger = getLogger(type);
            if (logger.IsDebugEnabled)
            {
                setProperties(type, exception);
                logger.Debug(string.Format(message, args), exception);
            }
        }

        public void Info(Type type, Exception exception = null, string message = "", params object[] args)
        {
            ILog logger = getLogger(type);
            if (logger.IsInfoEnabled)
            {
                setProperties(type, exception);
                logger.Info(string.Format(message, args), exception);
            }
        }

        public void Warning(Type type, Exception exception = null, string message = "", params object[] args)
        {
            ILog logger = getLogger(type);
            if (logger.IsWarnEnabled)
            {
                setProperties(type, exception);
                logger.Warn(string.Format(message, args), exception);
            }
        }

        public void Error(Type type, Exception exception = null, string message = "", params object[] args)
        {
            ILog logger = getLogger(type);
            if (logger.IsErrorEnabled)
            {
                setProperties(type, exception);
                logger.Error(string.Format(message, args), exception);
            }

        }

        public void Fatal(Type type, Exception exception = null, string message = "", params object[] args)
        {
            ILog logger = getLogger(type);
            if (logger.IsErrorEnabled)
            {
                setProperties(type, exception);
                logger.Fatal(string.Format(message, args), exception);
            }
        }

        public void Log(Type type, LogLevel level, Exception exception = null, string message = "", params object[] args)
        {

            switch (level)
            {
                case LogLevel.Trace:
                    Trace(type, exception, string.Format(message, args), args);
                    break;
                case LogLevel.Debug:
                    Debug(type, exception, string.Format(message, args), args);
                    break;
                case LogLevel.Info:
                    Info(type, exception, string.Format(message, args), args);
                    break;
                case LogLevel.Warning:
                    Warning(type, exception, string.Format(message, args), args);
                    break;
                case LogLevel.Error:
                    Error(type, exception, string.Format(message, args), args);
                    break;
                case LogLevel.Fatal:
                    Fatal(type, exception, string.Format(message, args), args);
                    break;
                default:
                    Info(type, exception, string.Format(message, args), args);
                    break;
            }
        }

        private void setProperties(Type source, Exception exception)
        {
            try
            {
                string assemblyProp = string.Empty;
                string classProp = string.Empty;
                string methodProp = string.Empty;
                string messageProp = string.Empty;
                string innerMessageProp = string.Empty;

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

                GlobalContext.Properties["LOGGER"] = assemblyProp;
                GlobalContext.Properties["CLASS"] = classProp;
                GlobalContext.Properties["METHOD"] = methodProp;
                GlobalContext.Properties["EXCEPTION"] = messageProp;
                GlobalContext.Properties["STACKTRACE"] = innerMessageProp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
