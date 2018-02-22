using bilisimHR.Common.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Tracing;
using NLogLogger = NLog;

namespace bilisimHR.Infrastructure.Logger.NLog
{
    public class APILogger : ITraceWriter
    {
        public APILogger()
        {
            string confFileTrace = System.Configuration.ConfigurationManager.AppSettings["LoggerConfigFile"] == null ? string.Empty : System.Configuration.ConfigurationManager.AppSettings["LoggerConfigFile"].ToString();
            string assemblyFolderTrace = AppDomain.CurrentDomain.RelativeSearchPath + @"\" + confFileTrace;
            LogManager.Configuration = new XmlLoggingConfiguration(assemblyFolderTrace, false);
        }

        private static readonly NLogLogger.Logger _classLogger = LogManager.GetCurrentClassLogger();

        private static readonly Lazy<Dictionary<TraceLevel, Action<LogEventInfo>>> _loggingMap =
            new Lazy<Dictionary<TraceLevel, Action<LogEventInfo>>>(() => new Dictionary<TraceLevel, Action<LogEventInfo>> {
                { TraceLevel.Info, _classLogger.Info },
                { TraceLevel.Debug, _classLogger.Debug },
                { TraceLevel.Error, _classLogger.Error },
                { TraceLevel.Fatal, _classLogger.Fatal },
                { TraceLevel.Warn, _classLogger.Warn }
            });

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
        public void Trace(HttpRequestMessage request, string module, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (!module.StartsWith("bilisimHR"))
                return;

            string message = string.Empty;
            //JToken Jtoken = JObject.Parse(traceAction.Target.ToJSON());
            JToken Jtoken = JObject.Parse(JsonConvert.SerializeObject(traceAction.Target));
            JToken subToken = Jtoken.SelectToken("messageArguments");
            JToken exceptionObj = Jtoken.SelectToken("exception");

            if (subToken == null)
                return;

            object jsonObj = subToken.First.ToObject(typeof(ApiLog));
            if (level != TraceLevel.Off && jsonObj.GetType() == typeof(ApiLog))
            {
                ApiLog apiLog = (ApiLog)jsonObj;
                if (traceAction != null && traceAction.Target != null)
                {
                    apiLog.Application = module;
                    message = "Application : " + apiLog.Application +
                        Environment.NewLine + "UserId : " + apiLog.UserId +
                        Environment.NewLine + "Username : " + apiLog.Username +
                        Environment.NewLine + "Machine : " + apiLog.Machine +
                        Environment.NewLine + "Controller : " + apiLog.Controller +
                        Environment.NewLine + "Action : " + apiLog.Action +
                        Environment.NewLine + "RequestIpAddress : " + apiLog.RequestIpAddress +
                        Environment.NewLine + "RequestContentType : " + apiLog.RequestContentType +
                        Environment.NewLine + "RequestContentBody : " + apiLog.RequestContentBody +
                        Environment.NewLine + "RequestUri : " + apiLog.RequestUri +
                        Environment.NewLine + "RequestMethod : " + apiLog.RequestMethod +
                        Environment.NewLine + "RequestRouteTemplate : " + apiLog.RequestRouteTemplate +
                        Environment.NewLine + "RequestRouteData : " + apiLog.RequestRouteData +
                        Environment.NewLine + "RequestHeaders : " + apiLog.RequestHeaders +
                        Environment.NewLine + "RequestTimestamp : " + (apiLog.RequestTimestamp.HasValue ? apiLog.RequestTimestamp.Value.ToString("dd'/'MM'/'yyyy HH:mm:ss") : string.Empty) +
                        Environment.NewLine + "ResponseContentType : " + apiLog.ResponseContentType +
                        Environment.NewLine + "ResponseContentBody : " + apiLog.ResponseContentBody +
                        Environment.NewLine + "ResponseStatusCode : " + (apiLog.ResponseStatusCode.HasValue ? apiLog.ResponseStatusCode.Value.ToJSON() : string.Empty) +
                        Environment.NewLine + "ResponseHeaders : " + apiLog.ResponseHeaders +
                        Environment.NewLine + "ResponseTimestamp : " + (apiLog.ResponseTimestamp.HasValue ? apiLog.ResponseTimestamp.Value.ToString("dd'/'MM'/'yyyy HH:mm:ss") : string.Empty) +
                        Environment.NewLine + "TotalExecutionTime: " + apiLog.TotalExecutionSeconds.ToString() +
                        Environment.NewLine + "-----------------------------------------------------------------------------";
                }

                var record = new TraceRecord(request, module, level);

                if (traceAction != null)
                    traceAction(record);

                log(record, apiLog, (exceptionObj != null ? ((Exception)exceptionObj.ToObject(typeof(Exception))) : null), message);
            }
        }

        /// <summary>
        /// Logs info/Error to Log file
        /// </summary>
        /// <param name="record"></param>
        private void log(TraceRecord record, ApiLog apiLog, Exception exception, string message)
        {
            //var message = new StringBuilder();
            LogEventInfo logDetailInfo = new LogEventInfo(LogLevel.FromString(record.Level.ToString()), "BilisimHRWebApiLogger", message);
            logDetailInfo.Exception = exception;
                        
            if (apiLog != null)
            {
                logDetailInfo.Properties["APPLICATION"] = apiLog.Application;
                logDetailInfo.Properties["USER_ID"] = apiLog.UserId;
                logDetailInfo.Properties["USERNAME"] = apiLog.Username;
                logDetailInfo.Properties["MACHINE"] = apiLog.Machine;
                logDetailInfo.Properties["CONTROLLER"] = apiLog.Controller;
                logDetailInfo.Properties["ACTION"] = apiLog.Action;
                logDetailInfo.Properties["REQUEST_IP_ADDRESS"] = apiLog.RequestIpAddress;
                logDetailInfo.Properties["REQUEST_CONTENT_TYPE"] = apiLog.RequestContentType;
                logDetailInfo.Properties["REQUEST_CONTENT_BODY"] = apiLog.RequestContentBody;
                logDetailInfo.Properties["REQUEST_URI"] = apiLog.RequestUri;
                logDetailInfo.Properties["REQUEST_METHOD"] = apiLog.RequestMethod;
                logDetailInfo.Properties["REQUEST_ROUTE_TEMPLATE"] = apiLog.RequestRouteTemplate;
                logDetailInfo.Properties["REQUEST_ROUTE_DATA"] = apiLog.RequestRouteData;
                logDetailInfo.Properties["REQUEST_HEADERS"] = apiLog.RequestHeaders;
                logDetailInfo.Properties["REQUEST_TIMESTAMP"] = (apiLog.RequestTimestamp.HasValue ? apiLog.RequestTimestamp.Value.ToString("dd'/'MM'/'yyyy HH:mm:ss") : string.Empty);
                logDetailInfo.Properties["RESPONSE_CONTENT_TYPE"] = apiLog.ResponseContentType;
                // Response Content Body veri tabanında büyük alana karşılık geldiğinden eklenmesi kapatılmıştır.
                // logDetailInfo.Properties["RESPONSE_CONTENT_BODY"] = apiLog.ResponseContentBody;
                logDetailInfo.Properties["RESPONSE_STATUS_CODE"] = (apiLog.ResponseStatusCode.HasValue ? apiLog.ResponseStatusCode.Value.ToJSON() : string.Empty);
                logDetailInfo.Properties["RESPONSE_HEADERS"] = apiLog.ResponseHeaders;
                logDetailInfo.Properties["RESPONSE_TIMESTAMP"] = (apiLog.ResponseTimestamp.HasValue ? apiLog.ResponseTimestamp.Value.ToString("dd'/'MM'/'yyyy HH:mm:ss") : string.Empty);
                logDetailInfo.Properties["TOTAL_EXECUTION_SECONDS"] = apiLog.TotalExecutionSeconds.ToString();
                // _logger[record.Level](logDetailInfo);
                _classLogger.Log(logDetailInfo);
            }
        }
    }
}
