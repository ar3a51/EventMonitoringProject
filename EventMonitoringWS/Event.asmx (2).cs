using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Services;
using System.Xml;
using System.Diagnostics;
using System.Net.Configuration;
using DataLayer;
using WebServiceCentre.Wrapper;
using WebServiceCentre.Email;
using WebServiceCentre.Logger;


namespace WebServiceCentre
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Event : System.Web.Services.WebService
    {
        private const String RESULT_NODE = "<result></result>";

        Configuration configurationFile; 
        MailSettingsSectionGroup mailSettings; 
        
        private String connectionString = "";
        private String displayName = "";

        public Event()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            this.configurationFile = WebConfigurationManager.OpenWebConfiguration("/WebServiceCentre/Web.config");
            this.mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            this.displayName = ConfigurationManager.AppSettings["Display"];

        }

        [WebMethod]
        public string testConnection()
        {
            return "success";
        }

        [WebMethod]
        public ResultSet logEvent(String strEventType,
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




                using (DataLayer.EventLogDataLayer layer = new EventLogDataLayer(this.connectionString))
                {
                    
                    /*
                    strEventType = xmlDoc.SelectSingleNode("//request//eventtype").InnerText;
                    strEventMessage = xmlDoc.SelectSingleNode("//request//eventmessage").InnerText;
                    strMachineName = xmlDoc.SelectSingleNode("//request//machinename").InnerText;
                    strUsername = xmlDoc.SelectSingleNode("//request//username").InnerText;
                    dtDateTime = DateTime.Parse(xmlDoc.SelectSingleNode("//request//datetimegenerated").InnerText);*/
                    using (DataLayer.UserDataLayer userLayer = new UserDataLayer(this.connectionString))
                    {
                       IEnumerable<DataLayer.Model.UserDetails> users = userLayer.getUsers(strMachineName);

                       foreach (DataLayer.Model.UserDetails user in users)
                       {
                           emailAddresses += user.sPrimaryEmail + ",";
                       }
                    }

                    if (emailAddresses != "")
                    {
                       
                        this.notifyServerAdmin(emailAddresses,
                                            strMachineName,
                                            strEventMessage,
                                            strUsername,
                                            strSource);
                    }

                    if (!layer.insertLogData(strEventType, strEventMessage, strMachineName, strUsername,strSource, dtTimeGenerated))
                     {
                         // Problem when trying to add data to database.
                         throw new Exception("Problem encountered.  Please notify the administrator.");
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
                this.sendExceptiontoAdmin(exception);
               
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
        public ResultSet addNewMachine(String type,
                                       String machineName,
                                       String environment)
        {
            
            try
            {

                using (DataLayer.EventLogDataLayer layer = new EventLogDataLayer(connectionString))
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

                this.sendExceptiontoAdmin(exception);

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
                using (DataLayer.EventLogDataLayer dataLayer = new EventLogDataLayer(this.connectionString))
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
                this.sendExceptiontoAdmin(exception);

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
            if (this.configurationFile == null)
                this.configurationFile = WebConfigurationManager.OpenWebConfiguration("/WebServiceCentre/Web.config");

            if (this.mailSettings == null)
                this.mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            if (this.displayName == null)
                this.displayName = ConfigurationManager.AppSettings["Display"];

            using (Logging log = new Logging())
            {
                /* log.logActivity("Exception message:\r\n " + exception.Message + "\r\n"
                                   + "Stack Trace:\r\n" + exception.StackTrace + "\r\n"
                                   + "Source:\r\n" + exception.Source + "\r\n"
                                   + "Additional Data:\r\n" + exception.Data + "\r\n"
                                   + "Time Generated:\r\n" + DateTime.Now + "\r\n", EventLogEntryType.Error);*/


                log.logActivity("value of smtp server: " + this.mailSettings.Smtp.Network.Host, EventLogEntryType.Error);
            }

            return new EmailNotification(this.mailSettings.Smtp.Network.Host,
                                                                         this.mailSettings.Smtp.From,
                                                                         this.displayName);
        }

        private void sendExceptiontoAdmin(Exception exception)
        {
            /*
            using (EmailNotification notify = this.getEmailObject())
            {
                notify.setRecipient(ConfigurationManager.AppSettings["Admin"]);
                notify.setSubject("Web Service Centre Alert");
                notify.setMessage("Exception message:\r\n " + exception.Message + "\r\n"
                                  + "Stack Trace:\r\n" + exception.StackTrace + "\r\n"
                                  + "Source:\r\n" + exception.Source + "\r\n"
                                  + "Additional Data:\r\n" + exception.Data + "\r\n"
                                  + "Time Generated:\r\n" + DateTime.Now + "\r\n");

                notify.sendMail();

            }*/

           

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