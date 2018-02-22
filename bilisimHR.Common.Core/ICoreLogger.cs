using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilisimHR.Common.Core
{
    public enum LogLevel
    {
        Trace = 0,
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public interface ICoreLogger
    {
        void Trace(Type type, Exception exception = null, string message = "", params object[] args);

        void Debug(Type type, Exception exception = null, string message = "", params object[] args);

        void Info(Type type, Exception exception = null, string message = "", params object[] args);

        void Warning(Type type, Exception exception = null, string message = "", params object[] args);

        void Error(Type type, Exception exception = null, string message = "", params object[] args);

        void Fatal(Type type, Exception exception = null, string message = "", params object[] args);

        void Log(Type type, LogLevel level, Exception exception = null, string message = "", params object[] args);
    }
}
