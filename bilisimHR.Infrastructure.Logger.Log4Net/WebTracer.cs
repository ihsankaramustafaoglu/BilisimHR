using bilisimHR.Common.Core.ErrorHandling;
using bilisimHR.Common.Helper;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;

namespace bilisimHR.Infrastructure.Logger.Log4Net
{
    public class WebTracer : ITraceWriter
    {
        private static string _configurationFile;
        private static readonly Dictionary<Type, ILog> _loggers = new Dictionary<Type, ILog>();
        private static readonly object _lock = new object();

        public WebTracer()
        {
            string confFileTrace = System.Configuration.ConfigurationManager.AppSettings["TracerConfigFile"] == null ? string.Empty : System.Configuration.ConfigurationManager.AppSettings["TracerConfigFile"].ToString();
            string assemblyFolderTrace = AppDomain.CurrentDomain.RelativeSearchPath + @"\" + confFileTrace;
            _configurationFile = assemblyFolderTrace;
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

        /// <summary>
        /// Implementation of TraceWriter to trace the logs.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <param name="traceAction"></param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var dictionary = category.Split(',').Select(part => part.Split('@')).Where(part => part.Length == 2).ToDictionary(sp => sp[0], sp => sp[1]);

            if (level != TraceLevel.Off)
            {
                if (traceAction != null && traceAction.Target != null)
                {
                    string message = string.Empty;
                    if (dictionary.Count != 0)
                    {
                        message = "IP Address : " + dictionary["IP_ADDRESS"] +
                        Environment.NewLine + "Controller : " + dictionary["CONTROLLER"] +
                        Environment.NewLine + "Action : " + dictionary["ACTION"] +
                        Environment.NewLine + "UserID : " + dictionary["USER_ID"] +
                        Environment.NewLine + "Action Params : " + traceAction.Target.ToJSON();

                        dictionary.Add("ACTION_PARAMETER", traceAction.Target.ToJSON());
                    }
                    category = message;
                    //category = category + Environment.NewLine + "Action Parametreleri : " + traceAction.Target.ToJSON();
                }

                var record = new TraceRecord(request, category, level);

                if (traceAction != null)
                    traceAction(record);

                log(record, dictionary, level);
            }
        }

        /// <summary>
        /// Logs info/Error to Log file
        /// </summary>
        /// <param name="record"></param>
        private void log(TraceRecord record, Dictionary<string, string> requestDict, TraceLevel level)
        {
            var message = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append("").Append(record.Message + Environment.NewLine);

            if (record.Request != null)
            {

                //GET, POST, PUT, DELETE
                if (record.Request.Method != null)
                {
                    GlobalContext.Properties["METHOD"] = record.Request.Method;
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);
                }


                if (record.Request.RequestUri != null)
                {
                    GlobalContext.Properties["URL"] = record.Request.RequestUri;
                    message.Append("").Append("URL: " + record.Request.RequestUri + Environment.NewLine);
                }


                if (record.Request.Headers != null && record.Request.Headers.Contains("Authorization") && record.Request.Headers.GetValues("Authorization") != null && record.Request.Headers.GetValues("Authorization").FirstOrDefault() != null)
                {
                    GlobalContext.Properties["AUTH_TOKEN"] = record.Request.Headers.GetValues("Authorization").FirstOrDefault();
                    message.Append("").Append("Auth Token: " + record.Request.Headers.GetValues("Authorization").FirstOrDefault() + Environment.NewLine);
                }
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append("").Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                var exceptionType = record.Exception.GetType();
                message.Append(Environment.NewLine);

                if (exceptionType == typeof(WebException))
                {
                    var exception = record.Exception as WebException;
                    if (exception != null)
                    {
                        message.Append("").Append("Hata Kodu: " + exception.HataKodu + Environment.NewLine);
                        message.Append("").Append("Hata: " + exception.HataAciklama + Environment.NewLine);
                        message.Append("").Append("Hata Detayı: " + exception.HataDetayAciklama + Environment.NewLine);
                        message.Append("").Append("Hata Stacktrace: " + exception.StackTrace + Environment.NewLine);
                        message.Append("").Append("HTTP Status Code: " + exception.HttpStatusCode + Environment.NewLine);
                    }
                }
                //else if (exceptionType == typeof(ApiBusinessException))
                //{
                //    var exception = record.Exception as ApiBusinessException;

                //    if (exception != null)
                //    {
                //        message.Append("").Append("Error: " + exception.ErrorDescription + Environment.NewLine);
                //        message.Append("").Append("Error Code: " + exception.ErrorCode + Environment.NewLine);
                //    }
                //}
                //else if (exceptionType == typeof(ApiDataException))
                //{
                //    var exception = record.Exception as ApiDataException;

                //    if (exception != null)
                //    {
                //        message.Append("").Append("Error: " + exception.ErrorDescription + Environment.NewLine);
                //        message.Append("").Append("Error Code: " + exception.ErrorCode + Environment.NewLine);
                //    }
                //}
                else
                {
                    message.Append("").Append("Hata: " + record.Exception.GetBaseException().Message + Environment.NewLine);
                    message.Append("").Append("Hata Stacktrace: " + record.Exception.StackTrace + Environment.NewLine);
                }

            }

            string logMessage = Convert.ToString(message) + Environment.NewLine;
            GlobalContext.Properties["IP_ADDRESS"] = requestDict["IP_ADDRESS"];
            GlobalContext.Properties["CONTROLLER"] = requestDict["CONTROLLER"];
            GlobalContext.Properties["ACTION"] = requestDict["ACTION"];
            GlobalContext.Properties["USER_ID"] = requestDict["USER_ID"];
            GlobalContext.Properties["ACTION_PARAMETER"] = requestDict["ACTION_PARAMETER"];

            log(level, logMessage);
        }

        private void log(TraceLevel level, string message)
        {
            ILog logger = getLogger(GetType());
            switch (level)
            {
                case TraceLevel.Debug:
                    logger.Debug(message);
                    break;
                case TraceLevel.Info:
                    logger.Info(message);
                    break;
                case TraceLevel.Warn:
                    logger.Warn(message);
                    break;
                case TraceLevel.Error:
                    logger.Error(message);
                    break;
                case TraceLevel.Fatal:
                    logger.Fatal(message);
                    break;
                default:
                    logger.Info(message);
                    break;
            }
        }

    }
}
