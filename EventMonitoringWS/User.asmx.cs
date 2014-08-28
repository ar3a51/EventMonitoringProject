using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Services;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Configuration;
using EventMonitoringWS.Wrapper;
using EventMonitoringWS.Email;
using DataLayer;
using DataLayer.Model;

namespace EventMonitoringWS
{
    /// <summary>
    /// Summary description for User
    /// </summary>
    [WebService(Namespace = "http://ICAA.EventMonitoringService.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class User : System.Web.Services.WebService
    {

        readonly Configuration  configurationFile; 
        readonly MailSettingsSectionGroup mailSettings;

        private readonly String connectionString = string.Empty;
        private readonly String displayName = string.Empty;

        public User()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            configurationFile = WebConfigurationManager.OpenWebConfiguration("/WebServiceCentre/Web.config");
            mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            displayName = ConfigurationManager.AppSettings["Display"];
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
                using (var user = new UserDataLayer(connectionString))
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
                    sendExceptiontoAdmin(exception);
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

        private EmailNotification getEmailObject()
        {
            return new EmailNotification(mailSettings.Smtp.Network.Host,
                                                                         mailSettings.Smtp.From,
                                                                         displayName);
            
        }


        private void notifyServerAdmin(String recipientAddress,
                                       String machineName,
                                       String message,
                                       String userName,
                                       String source)
        {
            using (var notify = getEmailObject())
            {
                notify.setRecipient(recipientAddress);
                notify.setSubject(machineName + " Alert");
                notify.setMessage(string.Format("Message:\r\n {0}\r\n" + "Source:\r\n {1}\r\n" + "User Name:\r\n{2}\r\n", message, source, userName));
                notify.sendMail();
            }

        }

        [WebMethod]
        public ResultMachines getMachines()
        {
            var user = new UserDataLayer(connectionString);
            
            return new ResultMachines{ 
                machines= user.getMachines().ToArray(),
                stringResultData = string.Empty,
                stringResultType= ResultSet.SUCCESS,
                stringResultMessage = ""
            };
            
        }

        [WebMethod]
        public UserDetails getUserDetails(String strUsername)
        {
            var user = new UserDataLayer(connectionString);

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

                using (var userDl = new UserDataLayer(connectionString))
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
                    stringResultData = string.Empty,
                    stringResultMessage = string.Empty,
                    stringResultType = ResultSet.SUCCESS
                };
            }
            catch (Exception exception)
            {
                
                
                sendExceptiontoAdmin(exception);
                
                return new ResultSet
                {
                    stringResultData = string.Empty,
                    stringResultMessage = exception.Message,
                    stringResultType = ResultSet.FAIL
                };
            }


        }

        [WebMethod]
        public resultUser addSuppressedLog(String operation, String chUsername, LogDetails[] log)
        {
            try
            {
                var userDL = new UserDataLayer(connectionString);

                if (operation.ToLower() == "insert")
                {
                    if (!userDL.addSuppressedLog(chUsername, log))
                        throw new Exception("Failed adding the data");
                }
                else
                {
                    if (!userDL.modifySuppressedLog(chUsername, log))
                        throw new Exception("Failed modify the data");
                }

                DataTable queryResult = retrieveSuppressedLog(chUsername, userDL);

                return new resultUser
                {
                    stringResultType = ResultSet.SUCCESS,
                    stringResultMessage = queryResult.Rows.Count.ToString(CultureInfo.InvariantCulture),
                    suppressedLog = queryResult
                };
                 
            }
            catch (Exception exception)
            {
                sendExceptiontoAdmin(exception);

                return new resultUser
                {
                    stringResultData = ResultSet.FAIL,
                    stringResultMessage = ""

                };
            }
          
        }

       

        [WebMethod]
        public resultUser deleteSuppressedLog(String username, LogDetails[] log)
        {
            try
            {
                var userDL = new UserDataLayer(connectionString);
                if (!userDL.removeSuppressedLog(username, log))
                    throw new Exception("Failed removing the data");


                DataTable resultTable = retrieveSuppressedLog(username, userDL);

                return new resultUser
                {
                    stringResultType = ResultSet.SUCCESS,
                    suppressedLog = resultTable,
                    stringResultMessage = resultTable.Rows.Count.ToString(CultureInfo.InvariantCulture)
                     
                };
            }
            catch (Exception ex)
            {
                this.sendExceptiontoAdmin(ex);
                return new resultUser
                {
                    stringResultType = ResultSet.FAIL,
                    stringResultMessage = ex.Message
                };
            }
        }
      
        private DataTable retrieveSuppressedLog(String chusername, UserDataLayer userdl)
        {
            var logTable = new DataTable("list");
            logTable.Columns.Add("EventId");
            logTable.Columns.Add("Source");

            foreach (var log in userdl.getListSuppressedLog(chusername).ToArray())
            {
                var row = logTable.NewRow();
                row["EventId"] = log.eventId;
                row["Source"] = log.source;

                logTable.Rows.Add(row);
            }

            return logTable;
           
        }

        [WebMethod]
        public resultUser getAllSuppressedLog(String userName)
        {
            var userDl = new UserDataLayer(connectionString);

            DataTable resultTable = retrieveSuppressedLog(userName, userDl);

            return new resultUser
            {

                stringResultType = ResultSet.SUCCESS,
                stringResultMessage = resultTable.Rows.Count.ToString(CultureInfo.InvariantCulture),
                suppressedLog = resultTable
            };
            //return resultTable;
        }

        [WebMethod]
        public ResultSet modifySuppressedLogList(String chUsername, LogDetails[] log)
        {
            try
            {

                var userDL = new UserDataLayer(connectionString);
                if (!userDL.modifySuppressedLog(chUsername, log))
                    throw new Exception("Failed modifying data");

                DataTable resultTable = retrieveSuppressedLog(chUsername, userDL);
                return new resultUser
                {
                    stringResultType = ResultSet.SUCCESS,
                    stringResultMessage = resultTable.Rows.Count.ToString(CultureInfo.InvariantCulture),
                    stringResultData = string.Empty,
                    suppressedLog = retrieveSuppressedLog(chUsername, userDL)
                };
            }
            catch (Exception exception)
            {
                this.sendExceptiontoAdmin(exception);

                return new ResultSet
                {
                    stringResultType = ResultSet.FAIL,
                    stringResultMessage = exception.Message,
                    stringResultData = string.Empty
                };

            }
        }

    }
}
