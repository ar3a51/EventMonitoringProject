using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using EventMonitoringService.EventMonitoring;


namespace EventMonitoringService
{
    public partial class EventMonitorService : ServiceBase
    {
        private EventMonitor monitor;
        public EventMonitorService()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            this.monitor = new EventMonitor();
            this.monitor.executeMonitoring();
        }

        protected override void OnStop()
        {
            this.monitor.Dispose();
            this.monitor = null;

        }
    }
}
