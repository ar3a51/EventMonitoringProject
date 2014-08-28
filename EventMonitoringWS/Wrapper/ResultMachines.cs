using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.Model;

namespace EventMonitoringWS.Wrapper
{
    public class ResultMachines : ResultSet
    {
        public Machine[] machines { set; get; }
    }
}