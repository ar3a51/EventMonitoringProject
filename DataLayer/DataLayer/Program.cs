using System;
using DataLayer;


namespace dllTester
{
    class Program
    {
        static void Main(string[] args)
        {



            try
            {

                using (DataLayer.EventLogDataLayer logger = new EventLogDataLayer("Server=localhost;Database=devdb;Integrated Security=true"))
                {
                    /*String[] sMachines;
                    sMachines = new String[5];
                    
                    sMachines[0] = "ca-tst-ocp-01";
                       sMachines[1] = "ca-tst-oep-101";
                        sMachines[2] = "ca-syd-oep-01";
                        sMachines[3] = "ca-syd-oxc-101";
                        sMachines[4] = "ca-uat-oxd-101";

                        userDl.modifyUserSetting(@"ca\hanggara", "hanggara", "Prameswara", "me@nowwhere.net", "me@nowhere.net", sMachines);*/
                    //logger.insertLogData(125, "lskjdfl", "ksljdfl", "kalsd", "sldfjlasdfj", "sldjslf", DateTime.Now);

                   // Console.WriteLine("success");
                   // LogDetails[] logs;

                    //logs = logger.getLogData("kalsd");

                  /*  foreach(var log in logs)
                    {
                       // Console.WriteLine(obj._iEventId);
                        Console.WriteLine(log.eventId + " " + log.source);
                    }
                    Console.ReadLine();*/
                }

              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        /*
        static void client_logEventCompleted(object sender, EventLogger.logEventCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            EventLogger.ResultSet result = (EventLogger.ResultSet)e.Result;
            Console.WriteLine(result.stringResultType);
            Console.ReadLine();
        }

        static void client_testConnectionCompleted(object sender, EventLogger.testConnectionCompletedEventArgs e)
        {
           
            //throw new NotImplementedException();
           
        }

        private static void saveToXML(
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
            XmlNode typeNode = null;
            XmlNode messageNode = null;
            XmlNode userNode = null;
            XmlNode sourceNode = null;
            XmlNode timeGenNode = null;

            XmlDocument xmlDocument = null;



            if (!File.Exists("eventInfo.xml"))
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
                xmlDocument.Load("eventInfo.xml");
                rootElement = xmlDocument.DocumentElement;
            }

            //create the basis error element
            errorNode = xmlDocument.CreateElement("error");

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



            xmlDocument.Save("eventInfo.xml");

            xmlDocument = null;
        }
      }
    */
    }
}

