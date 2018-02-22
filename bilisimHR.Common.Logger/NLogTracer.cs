using bilisimHR.Common.Core.ErrorHandling;
using bilisimHR.Common.Helper;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;

namespace bilisimHR.Common.Logger
{
    public class NLogTracer : ITraceWriter
    {
        public NLogTracer(string configurationFile)
        {
            LogManager.Configuration = new XmlLoggingConfiguration(configurationFile, true);
        }

        private static readonly NLog.Logger _classLogger = LogManager.GetCurrentClassLogger();

        private static readonly Lazy<Dictionary<TraceLevel, Action<LogEventInfo>>> _loggingMap =
            new Lazy<Dictionary<TraceLevel, Action<LogEventInfo>>>(() => new Dictionary<TraceLevel, Action<LogEventInfo>> {
                { TraceLevel.Info, _classLogger.Info },
                { TraceLevel.Debug, _classLogger.Debug },
                { TraceLevel.Error, _classLogger.Error },
                { TraceLevel.Fatal, _classLogger.Fatal },
                { TraceLevel.Warn, _classLogger.Warn } });

        private Dictionary<TraceLevel, Action<LogEventInfo>> _logger
        {
            get { return _loggingMap.Value; }
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
                    string message = "IP Address : " + dictionary["IP_ADDRESS"] +
                        Environment.NewLine + "Controller : " + dictionary["CONTROLLER"] +
                        Environment.NewLine + "Action : " + dictionary["ACTION"] +
                        Environment.NewLine + "UserID : " + dictionary["USER_ID"] +
                        Environment.NewLine + "Action Params : " + traceAction.Target.ToJSON();

                    dictionary.Add("ACTION_PARAMETER", traceAction.Target.ToJSON());
                    category = message;
                    //category = category + Environment.NewLine + "Action Parametreleri : " + traceAction.Target.ToJSON();
                }

                var record = new TraceRecord(request, category, level);

                if (traceAction != null)
                    traceAction(record);

                log(record, dictionary);
            }
        }

        /// <summary>
        /// Logs info/Error to Log file
        /// </summary>
        /// <param name="record"></param>
        private void log(TraceRecord record, Dictionary<string, string> requestDict)
        {
            var message = new StringBuilder();
            LogEventInfo logDetailInfo = new LogEventInfo(LogLevel.FromString(record.Level.ToString()), "BilisimHRWebApiLogger", String.Empty);

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append("").Append(record.Message + Environment.NewLine);

            if (record.Request != null)
            {

                //GET, POST, PUT, DELETE
                if (record.Request.Method != null)
                {
                    logDetailInfo.Properties["METHOD"] = record.Request.Method;
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);
                }


                if (record.Request.RequestUri != null)
                {
                    logDetailInfo.Properties["URL"] = record.Request.RequestUri;
                    message.Append("").Append("URL: " + record.Request.RequestUri + Environment.NewLine);
                }


                if (record.Request.Headers != null && record.Request.Headers.Contains("Authorization") && record.Request.Headers.GetValues("Authorization") != null && record.Request.Headers.GetValues("Authorization").FirstOrDefault() != null)
                {
                    logDetailInfo.Properties["AUTH_TOKEN"] = record.Request.Headers.GetValues("Authorization").FirstOrDefault();
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

            logDetailInfo.Message = Convert.ToString(message) + Environment.NewLine;
            logDetailInfo.Properties["IP_ADDRESS"] = requestDict["IP_ADDRESS"];
            logDetailInfo.Properties["CONTROLLER"] = requestDict["CONTROLLER"];
            logDetailInfo.Properties["ACTION"] = requestDict["ACTION"];
            logDetailInfo.Properties["USER_ID"] = requestDict["USER_ID"];
            logDetailInfo.Properties["ACTION_PARAMETER"] = requestDict["ACTION_PARAMETER"];

            _logger[record.Level](logDetailInfo);
        }

    }
}
