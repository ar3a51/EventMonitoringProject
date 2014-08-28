using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventMonitoringWS.Wrapper
{
    public class ResultLog : ResultSet
    {
        public DataTable logList { set; get; }
        public String numberOfRecords { set; get; }
    }
}