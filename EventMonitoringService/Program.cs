using System.ServiceProcess;

namespace EventMonitoringService
{
    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var servicesToRun = new ServiceBase[]
                                              {
                                                  new EventMonitorService()
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}