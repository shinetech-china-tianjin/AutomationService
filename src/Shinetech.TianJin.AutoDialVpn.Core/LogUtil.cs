using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Shinetech.TianJin.AutoDialVpn.Core
{
    internal class LogUtil
    {
        private const string EventSourceName = "AutoDialVpn";
        private const string EventLogName = "AutoDialVpnLog";
        private static EventLog _log = new EventLog();

        static LogUtil() {
            if (!EventLog.SourceExists(EventSourceName)) {
                EventLog.CreateEventSource(EventSourceName, EventLogName);
            }
            _log.Source = EventSourceName;
            _log.Log = EventLogName;
        }

        internal static void WriteError(string error) {
            _log.WriteEntry(error, EventLogEntryType.Error);
        }
        internal static void WriteInfo(string info) {
            _log.WriteEntry(info, EventLogEntryType.Information);
        }
    }
}
