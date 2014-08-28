using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace EventMonitoringAdmin.Models.AppDomain
{
    public class EventDetail
    {
        public int numberOfRecords { set; get; }
        public DataTable eventList { set; get; }
       
    }
}