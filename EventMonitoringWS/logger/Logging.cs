using System;
using System.Diagnostics;

namespace EventMonitoringWS.Logger
{
    class Logging : IDisposable
    {
       private readonly String _strSource;
       private readonly String _strLog;
       private readonly int _intAppId;
      

        public Logging()
        {
             _strSource = "EventMonitoringWS";
             _strLog = "Application";
             _intAppId = 257;
        }

        public void logActivity(String strActivity, EventLogEntryType entryType)
        {
        
            if (!EventLog.SourceExists(_strLog))
                EventLog.CreateEventSource(_strSource, _strLog);

            EventLog.WriteEntry(_strSource, strActivity, entryType,_intAppId);

        }

        public void Dispose()
        {

        }
    }
}
