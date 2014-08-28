using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Xml;




namespace EventMonitoringService.EventMonitoring
{
    
    class EventMonitor : IDisposable
    {
        private EventLog anEvent;
        private EventLogger.ResultSet result;
        private EventLogger.EventSoapClient service;
        private String logPath;
        private String severity;
        private String strUsername = "N/A";
        private String strEntryType = "N/A";
        private String strMessage = "N/A";
        private String strMachineName = "N/A";
        private String strSource = "N/A";
        private String strTimeGenerated = "1900-01-01";
        private long eventId = 0;


        public EventMonitor()
        {
            this.anEvent = new EventLog();
            this.anEvent.Log = ConfigurationManager.AppSettings["logToMonitor"]; 
            this.logPath = ConfigurationManager.AppSettings["logPath"];
            

            this.service = new EventLogger.EventSoapClient();

            this.service.addNewMachine(ConfigurationManager.AppSettings["machineType"],
                                       this.GetFQDN(),
                                       ConfigurationManager.AppSettings["environment"]);

            this.anEvent.Disposed += new EventHandler(anEvent_Disposed);
            
            
        }

        void anEvent_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
           
        }

        /// <summary>
        /// 
        /// </summary>
        public void executeMonitoring()
        {
           
            this.anEvent.EnableRaisingEvents = true;
            this.anEvent.EntryWritten += new EntryWrittenEventHandler(event_entryWritten);
            this.service.logEventCompleted += new EventHandler<EventLogger.logEventCompletedEventArgs>(service_logEventCompleted);
            
            
        }

        void service_logEventCompleted(object sender, EventLogger.logEventCompletedEventArgs e)
        {
            try
            {
                this.result = (EventLogger.ResultSet)e.Result;

                if (File.Exists(this.logPath)) // if xml log exists then there's a problem connecting to the web service
                {
                    if (this.service.testConnection() == "success") //check the connection.  If succeed then loads all the error from xml to web service
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(this.logPath);
                        XmlNodeList nodes = document.SelectNodes(@"/Errors/error");
                        foreach (XmlNode node in nodes)
                        {
                            this.service.logEvent(
                                                   long.Parse(node["eventId"].InnerText),
                                                   node["type"].InnerText,
                                                   node["message"].InnerText,
                                                   this.strMachineName,
                                                   node["username"].InnerText,
                                                   node["source"].InnerText,
                                                   node["timegenerated"].InnerText
                                                  );

                        }


                            File.Delete(this.logPath);
                    }
                }
                    
            }
            catch (Exception exception)
            {
                /*
                using (StreamWriter write = new StreamWriter(this.logPath, true))
                {
                    write.WriteLine(exception.GetType().ToString());
                    write.WriteLine(exception.Message);
                    write.WriteLine(exception.Data.ToString());
                }*/

                this.saveToXML((int)this.eventId,this.strEntryType, this.strMessage, this.strMachineName, this.strUsername, this.strSource, this.strTimeGenerated);
            }
        }

        void event_entryWritten(object sender, EntryWrittenEventArgs e)
        {
           
            EventLogEntry entry = e.Entry;

            //if (entry.InstanceId != 0)
                eventId = entry.InstanceId;
            

            if (entry.UserName != null)
                strUsername = entry.UserName;

            strEntryType = entry.EntryType.ToString();

            if (entry.Message != null)
                strMessage = entry.Message;

            //if (entry.MachineName != null)
            strMachineName = this.GetFQDN();

            if (entry.Source != null)
                strSource = entry.Source;

            if (entry.TimeGenerated != null)
                strTimeGenerated = entry.TimeGenerated.ToString();

            try
            {
                

                if (strEntryType.ToLower() == ConfigurationManager.AppSettings["severityToAlert"].ToLower())
                    this.service.logEventAsync((int)eventId,strEntryType, strMessage, strMachineName, strUsername, strSource, strTimeGenerated);                   
                            
            }
            catch (Exception exception)
            {
                
                //do nothing
                this.saveToXML((int)eventId,strEntryType, strMessage, strMachineName, strUsername, strSource, strTimeGenerated);
                

                   
            }

           
        }

        private string GetFQDN()
        {
            String hostName = "";
            String domainName = "";
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            //string domainName = NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            domainName = ipProperties.DomainName;
            hostName = ipProperties.HostName;
            string fqdn = "";
            if (domainName.Trim() != "")
                fqdn = hostName + "." + domainName;
            else
                fqdn = hostName;

            return fqdn;
        }


        private void saveToXML(
                                int eventId,
                               String strErrorType,
                               String strMessage,
                               String strMachineName,
                               String strUsername,
                               String strSource,
                               String strTimeGenerated
                             )
        {
            XmlElement rootElement = null;
            XmlDeclaration declaration = null;
            XmlNode errorNode = null;
            XmlNode eventIdNode = null;
            XmlNode typeNode = null;
            XmlNode messageNode = null;
            XmlNode userNode = null;
            XmlNode sourceNode = null;
            XmlNode timeGenNode = null;

            XmlDocument xmlDocument = null;



            if (!File.Exists(this.logPath))
            {
                xmlDocument = new XmlDocument();
                declaration = xmlDocument.CreateXmlDeclaration("1.0", null, null);
                xmlDocument.AppendChild(declaration);
                rootElement = xmlDocument.CreateElement("Errors");
                xmlDocument.AppendChild(rootElement);
            }
            else
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(this.logPath);
                rootElement = xmlDocument.DocumentElement;
            }
            
            //create the basis error element
            errorNode = xmlDocument.CreateElement("error");

            eventIdNode = xmlDocument.CreateElement("eventId");
            eventIdNode.InnerText = eventId.ToString();
            errorNode.AppendChild(eventIdNode);

            typeNode = xmlDocument.CreateElement("type");
            typeNode.InnerText = strErrorType;
            errorNode.AppendChild(typeNode);

            messageNode = xmlDocument.CreateElement("message");
            messageNode.InnerText = strMessage;
            errorNode.AppendChild(messageNode);
            

            userNode = xmlDocument.CreateElement("username");
            userNode.InnerText = strUsername;
            errorNode.AppendChild(userNode);

            sourceNode = xmlDocument.CreateElement("source");
            sourceNode.InnerText = strSource;
            errorNode.AppendChild(sourceNode);

            timeGenNode = xmlDocument.CreateElement("timegenerated");
            timeGenNode.InnerText = strTimeGenerated;
            errorNode.AppendChild(timeGenNode);

            rootElement.AppendChild(errorNode);



            xmlDocument.Save(this.logPath);

            xmlDocument = null;
        }
        
        public void Dispose()
        {
            this.service.removeMachine(this.GetFQDN());

            this.service = null;
            this.anEvent = null;
           
        }
    }
}
