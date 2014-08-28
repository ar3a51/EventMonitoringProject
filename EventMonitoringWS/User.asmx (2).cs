using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Configuration;
using WebServiceCentre.Wrapper;
using WebServiceCentre.Email;
using DataLayer;
using DataLayer.Model;

namespace WebServiceCentre
{
    /// <summary>
    /// Summary description for User
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class User : System.Web.Services.WebService
    {

        Configuration configurationFile; 
        MailSettingsSectionGroup mailSettings; 
        
        private String connectionString = "";
        private String displayName = "";

        public User()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            this.configurationFile = WebConfigurationManager.OpenWebConfiguration("/WebServiceCentre/Web.config");
            this.mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            this.displayName = ConfigurationManager.AppSettings["Display"];
        }

        [WebMethod]
        public ResultSet addNewUser(String userName, 
                                    String firstName, 
                                    String lastName, 
                                    String primaryEmail, 
                                    String secondaryEmail, 
                                    String[] machines)
        {
            Boolean flagNotify = false;
            //String[] machineCollections = machines.Split(',');
            try
            {
                using (DataLayer.UserDataLayer user = new UserDataLayer(this.connectionString))
                {

                    if (!user.addNewUser(userName, firstName, lastName, primaryEmail, secondaryEmail, machines))
                    {
                        flagNotify = true;
                        throw new Exception("Problem adding user to database.  Notify administrator");
                    }

                }

                return new ResultSet {  stringResultData = "", 
                                        stringResultMessage = "", 
                                        stringResultType = ResultSet.SUCCESS };
            }
            catch (Exception exception)
            {
                if (flagNotify)
                {
                    this.sendExceptiontoAdmin(exception);
                }
                return new ResultSet
                {
                    stringResultData = "",
                    stringResultMessage = exception.Message,
                    stringResultType = ResultSet.FAIL
                };
            }
        }

        private void sendExceptiontoAdmin(Exception exception)
        {

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

            }

        }

        private EmailNotification getEmailObject()
        {
            return new EmailNotification(this.mailSettings.Smtp.Network.Host,
                                                                         this.mailSettings.Smtp.From,
                                                                         this.displayName);
            
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
                                  + "User Name:\r\n" + userName + "\r\n");
                notify.sendMail();
            }

        }

        [WebMethod]
        public ResultMachines getMachines()
        {
            UserDataLayer user = new UserDataLayer(this.connectionString);
            
               
            

            return new ResultMachines{ 
                machines= user.getMachines().ToArray(),
                stringResultData = "",
                stringResultType= ResultSet.SUCCESS,
                stringResultMessage = ""
            };
            
        }

        [WebMethod]
        public UserDetails getUserDetails(String strUsername)
        {
            UserDataLayer user = new UserDataLayer(this.connectionString);

            UserDetails userDetail = user.getUserDetails(strUsername);
            user.Dispose();
            user = null;

            return userDetail;
        }

        [WebMethod]
        public ResultSet modifyUserSettings(String userName,
                                    String firstName,
                                    String lastName,
                                    String primaryEmail,
                                    String secondaryEmail,
                                    String[] machines)
        {
            try
            {
                

                using (UserDataLayer userDl = new UserDataLayer(this.connectionString))
                {
                    if (!userDl.modifyUserSetting(userName,
                                             firstName,
                                             lastName,
                                             primaryEmail,
                                             secondaryEmail,
                                             machines))
                    {
                        throw new Exception("Problem adding user to database.  Notify administrator");
                    }

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
                    stringResultData = "",
                    stringResultMessage = exception.Message,
                    stringResultType = ResultSet.FAIL
                };
            }


        }

        /*[WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }*/
    }
}
