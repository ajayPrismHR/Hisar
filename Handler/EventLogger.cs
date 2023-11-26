using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Handler
{
    public static class EventLogger
    {
        public static readonly Serilog.ILogger _log;
        static EventLogger()
        {
            _log = new LoggerConfiguration().
                    ReadFrom.AppSettings().
                    CreateLogger();
        }

    }
}