using System;
using EventMonitoringAdmin.EventManagementService;
using EventMonitoringAdmin.Models.AppDomain;

namespace EventMonitoringAdmin.Models
{
    public class EventModel
    {
        private Event _event;

        public EventModel()
        {
            _event = new Event();
        }

        public EventDetail getEventLogList(String machineName)
        {
            ResultLog result = _event.getLogInfo(machineName);

            return new EventDetail
            {
                 eventList = result.logList,
                 numberOfRecords = int.Parse(result.numberOfRecords)
            };
        }
    }
}