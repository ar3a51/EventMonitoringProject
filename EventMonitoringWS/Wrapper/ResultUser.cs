using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventMonitoringWS.Wrapper
{
    public class resultUser : ResultSet
    {
        public string userName { set; get; }
        public DataTable suppressedLog { set; get; }
    }

}