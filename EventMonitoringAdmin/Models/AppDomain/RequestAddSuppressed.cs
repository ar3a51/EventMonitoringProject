using System;
using System.Collections.Generic;
using System.Web;
using EventMonitoringAdmin.UserManagement;

namespace EventMonitoringAdmin.Models.AppDomain
{
    public class RequestAddSuppressed
    {
        public String userName { set; get; }
        public LogDetails[] log { set; get; }
    }
}