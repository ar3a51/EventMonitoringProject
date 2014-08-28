using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Services;
using System.Xml;
using System.Diagnostics;
using System.Net.Configuration;
using DataLayer;
using DataLayer.Model;
using EventMonitoringWS.Wrapper;
using EventMonitoringWS.Email;
using EventMonitoringWS.Logger;



namespace EventMonitoringWS
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://ICAA.EventMonitoringService.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Event : System.Web.Services.WebService
    {
        private const String RESULT_NODE = "<result></result>";

        readonly Configuration configurationFile;
        readonly MailSettingsSectionGroup mailSettings;

        private readonly String connectionString = string.Empty;
        private readonly String displayName = string.Empty;

        public Event()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            configurationFile = WebConfigurationManager.OpenWebConfiguration("/EventMonitoringWS/Web.config");
            mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            displayName = ConfigurationManager.AppSettings["Display"];

        }

        [WebMethod]
        public string testConnection()
        {
            return "success";
        }

        [WebMethod]
        public ResultSet logEvent(
                long eventId,
                String strEventType,
                String strEventMessage,
                String strMachineName,
                String strUsername,
                String strSource,
                String dtDateTime)
        {
            /*XmlDocument xmlDoc = null;
            XmlDocument xmlResultDoc = null;

            xmlDoc = new XmlDocument();
            xmlResultDoc = new XmlDocument();

            xmlResultDoc.LoadXml(RESULT_NODE);
            XmlNode node = xmlResultDoc.SelectSingleNode("result");
            XmlNode resNode = xmlResultDoc.CreateNode(XmlNodeType.Element, "status", null);

            xmlDoc.LoadXml(strXMLRequest);*/

            ResultSet result;
            String emailAddresses = "";

            try
            {



                /*String strEventType;
                String strEventMessage ;
                String strMachineName;
                String strUsername;*/
                DateTime dtTimeGenerated = DateTime.Parse(dtDateTime);




                using (var layer = new EventLogDataLayer(connectionString))
                {


                    if (!layer.insertLogData((UInt16)eventId,strEventType, strEventMessage, strMachineName, strUsername, strSource, dtTimeGenerated))
                    {
                        // Problem when trying to add data to database.
                        throw new Exception("Problem encountered.  Please notify the administrator.");
                    }
                    
                    /*
                    strEventType = xmlDoc.SelectSingleNode("//request//eventtype").InnerText;
                    strEventMessage = xmlDoc.SelectSingleNode("//request//eventmessage").InnerText;
                    strMachineName = xmlDoc.SelectSingleNode("//request//machinename").InnerText;
                    strUsername = xmlDoc.SelectSingleNode("//request//username").InnerText;
                    dtDateTime = DateTime.Parse(xmlDoc.SelectSingleNode("//request//datetimegenerated").InnerText);*/
                    using (var userLayer = new UserDataLayer(connectionString))
                    {
                        IEnumerable<UserDetails> users = userLayer.getUsers(strMachineName, (UInt16)eventId);

                        emailAddresses = users.Aggregate(emailAddresses, (current, user) => current + (user.sPrimaryEmail + ","));
                    }

                    if (emailAddresses != "")
                    {
                       
                        notifyServerAdmin(emailAddresses,
                                            strMachineName,
                                            strEventMessage,
                                            strUsername,
                                            strSource);
                    }

                   

                }

               

               // resNode.InnerText = SUCCESS;
                result =  new ResultSet
                {
                    stringResultData = "",
                    stringResultMessage = "",
                    stringResultType = ResultSet.SUCCESS
                };
                       
               

                
            }
            catch (Exception exception)
            {
                
                //return FAIL;
                //resNode.InnerText = FAIL;
                sendExceptiontoAdmin(exception);
               
                result = new ResultSet
                {
                    stringResultData = exception.StackTrace,
                    stringResultMessage = exception.Message,
                    stringResultType = ResultSet.FAIL
                };
            }


            return result;

            
        }

        [WebMethod]
        public ResultLog getLogInfo(String machineName)
        {
            DataTable resultData;

            using (var log = new EventLogDataLayer(connectionString))
            {
                resultData = new DataTable("logData");
                resultData.Columns.Add("EventId");
                resultData.Columns.Add("Source");
                resultData.Columns.Add("UserName");
                resultData.Columns.Add("dtGenerated");
                resultData.Columns.Add("Type");

               

                foreach (var logInfo in log.getLogData(machineName))
                {
                    var newRow = resultData.NewRow();

                    newRow["EventId"] = logInfo.eventId;
                    newRow["Source"] = logInfo.source;
                    newRow["UserName"] = logInfo.chUsername;
                    newRow["dtGenerated"] = logInfo.dtGenerated;
                    newRow["Type"] = logInfo.type;

                    resultData.Rows.Add(newRow);
                }

            }

           // return resultData;

           return new ResultLog
            {
                stringResultType = ResultSet.SUCCESS,
                numberOfRecords = resultData.Rows.Count.ToString(CultureInfo.InvariantCulture),
                logList = resultData
            };

            
        }
        
        [WebMethod]
        public ResultSet addNewMachine(String type,
                                       String machineName,
                                       String environment)
        {
            
            try
            {

                using (var layer = new EventLogDataLayer(connectionString))
                {
                    if (!layer.addNewMachine(type,machineName, environment))
                    {
                        // Problem when trying to add data to database.
                        throw new Exception("Problem encountered.  Please notify the administrator.");
                    }

                }

                //resNode.InnerText = SUCCESS;

                return new ResultSet
                {
                    stringResultData = "",
                    stringResultMessage = "",
                    stringResultType = ResultSet.SUCCESS
                };
            }
            catch (Exception exception)
            {
                //resNode.InnerText = FAIL;

                sendExceptiontoAdmin(exception);

                return new ResultSet
                {
                    stringResultData = exception.StackTrace,
                    stringResultMessage = exception.Message,
                    stringResultType = ResultSet.FAIL
                };

            }
           

            
        }

        [WebMethod]
        public ResultSet removeMachine(String machineName)
        {
            try
            {
                using (var dataLayer = new EventLogDataLayer(connectionString))
                {
                   if(!dataLayer.removeMachine(machineName))
                       throw new Exception("Problem encountered.  Please notify the administrator.");

                }

                return new ResultSet
                {
                    stringResultData = "",
                    stringResultMessage = "",
                    stringResultType = ResultSet.SUCCESS
                };
            }
            catch (Exception exception)
            {
                sendExceptiontoAdmin(exception);

                return new ResultSet
                {
                    stringResultData = exception.StackTrace,
                    stringResultMessage = exception.Message,
                    stringResultType = ResultSet.FAIL
                };
            }
        }

        private EmailNotification getEmailObject()
        {
           
            return new EmailNotification(mailSettings.Smtp.Network.Host,
                                                                         mailSettings.Smtp.From,
                                                                         displayName);
        }

        private void sendExceptiontoAdmin(Exception exception)
        {

            using (var notify = getEmailObject())
            {
                notify.setRecipient(ConfigurationManager.AppSettings["Admin"]);
                notify.setSubject("Web Service Centre Alert");
                notify.setMessage("Exception message:\r\n " + exception.Message + "\r\n"
                                  + "Stack Trace:\r\n" + exception.StackTrace + "\r\n"
                                  + "Source:\r\n" + exception.Source + "\r\n"
                                  + "Additional Data:\r\n" + exception.Data + "\r\n"
                                  + "Time Generated:\r\n" + DateTime.Now + "\r\n");

                notify.sendMail();

            }

        }

        private void notifyServerAdmin(String recipientAddress, 
                                       String machineName,
                                       String message,
                                       String userName,
                                       String source)
        {
            using (EmailNotification notify = getEmailObject())
            {
                notify.setRecipient(recipientAddress);
                notify.setSubject(machineName + " Alert");
                notify.setMessage("Message:\r\n " + message + "\r\n"
                                  + "Source:\r\n " + source + "\r\n"
                                  + "User Name:\r\n" + userName + "\r\n"
                                  + "Time Generated:\r\n" + DateTime.Now + "\r\n");
                notify.sendMail();
            }

        }
    }
}