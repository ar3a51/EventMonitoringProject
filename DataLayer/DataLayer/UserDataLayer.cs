using System;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using DataLayer.Model;
using DataLayer.Logging;

namespace DataLayer
{
    /// <summary>
    ///     Provide data access for user administration
    /// </summary>
    ///  <author>Hanggara Prameswara</author>
    ///  <date>05/07/2011</date>
   public class UserDataLayer:IDisposable
    {
        DataConDataContext oDataContext = null;

        private const int ERROR_DUPLICATE_ID = 2627;
        

        public UserDataLayer(String connectionString)
        {
            this.oDataContext = new DataConDataContext(connectionString);

        }

       /// <summary>
       ///      Adding new user to the monitoring system
       /// </summary>
       /// <param name="sUsername">User name for the user</param>
       /// <param name="sFirstname">User's first name</param>
        /// <param name="sLastname">User's last name</param>
        /// <param name="sPrimaryEmail">User's Primary e-mail address</param>
        /// <param name="sSecondaryEmail">User's Secondary e-mail address</param>
        /// <param name="sMachineNames">Collection of machines that user needs to monitor</param>
       /// <returns>True if adding new user successfully</returns>
        public bool addNewUser(String sUsername,
                              String sFirstname,
                              String sLastname,
                              String sPrimaryEmail,
                              String sSecondaryEmail,
                              String[] sMachineNames)
        {
            try
            {
               
                    tblUser eUser = new tblUser
                    {
                        chUsername = sUsername.Trim(),
                        vchFirstname = sFirstname.Trim(),
                        vchLastname = sLastname.Trim(),
                        vchPrimaryEmail = sPrimaryEmail.Trim(),
                        vchSecondaryEmail = sSecondaryEmail.Trim(),
                        dtInsertdate = DateTime.Now
                    };



                    foreach (String sMachineName in sMachineNames)
                    {
                        tblMonitoring oMonitor = new tblMonitoring
                        {
                            chMachineName = sMachineName.Trim(),
                            dtInserted = DateTime.Now

                        };

                        eUser.tblMonitorings.Add(oMonitor);
                        oMonitor = null;

                    }

                    this.oDataContext.tblUsers.InsertOnSubmit(eUser);
                    this.oDataContext.SubmitChanges();
               
                return true;
            }
            catch (SqlException oSqlException)
            {
                if (oSqlException.Errors[0].Number == ERROR_DUPLICATE_ID)
                {
                    throw new Exception("Username already exists");
                }
                else
                {
                    using (ErrorLogger oLogger = new ErrorLogger())
                    {
                        oLogger.dumpException(oSqlException, this.GetType());
                    }
                }
                return false;


            }
            catch (Exception oException)
            {
                using (ErrorLogger oLogger = new ErrorLogger())
                {
                    oLogger.dumpException(oException, this.GetType());
                }

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean addSuppressedLog(String chUsername, LogDetails[] log)
        {
            Boolean isSuccess;

            try
            {
                foreach (var logdetail in log)
                {
                    tblSuppressedNotification tblSuppressed = new tblSuppressedNotification
                    {
                        chUsername = chUsername,
                        iEventId = logdetail.eventId,
                        vchSource = logdetail.source
                    };

                    this.oDataContext.tblSuppressedNotifications.InsertOnSubmit(tblSuppressed);

                    tblSuppressed = null;
                }

                this.oDataContext.SubmitChanges();

                isSuccess = true;
            }
            catch (Exception exception)
            {
                using (ErrorLogger oLogger = new ErrorLogger())
                {
                    oLogger.dumpException(exception, this.GetType());
                }

                isSuccess = false;
            }

            return isSuccess;

        }


        public Boolean modifySuppressedLog(String chUsername, LogDetails[] log)
        {
            var querySuppressedLog = from suppressedLog in this.oDataContext.tblSuppressedNotifications
                                     where suppressedLog.chUsername == chUsername
                                     select suppressedLog;

           this.oDataContext.tblSuppressedNotifications.DeleteAllOnSubmit<tblSuppressedNotification>(querySuppressedLog);

           return this.addSuppressedLog(chUsername, log);
        }

        public Boolean removeSuppressedLog(String chUserName,LogDetails[] log)
        {
            try
            {
                foreach (LogDetails logDetails in log)
                {
                    var queryDeletedLog = from suppressedLog in this.oDataContext.tblSuppressedNotifications
                                          where suppressedLog.chUsername.Equals(chUserName)
                                          && suppressedLog.iEventId == logDetails.eventId
                                          select suppressedLog;
                    //var queryDeletedLog = this.oDataContext.tblSuppressedNotifications.Single(tblNotification => tblNotification.iEventId == logDetails.eventId
                    //                                                                          && tblNotification.chUsername.Equals(chUserName));


                    this.oDataContext.tblSuppressedNotifications.DeleteAllOnSubmit(queryDeletedLog);
                }

                this.oDataContext.SubmitChanges();
                //}
                return true;
            }
            catch (Exception exception)
            {
                using (ErrorLogger oLogger = new ErrorLogger())
                {
                    oLogger.dumpException(exception, this.GetType());
                }

                return false;
            }
                                  
        }

        public IEnumerable<LogDetails> getListSuppressedLog(String chUsername)
        {

            
            /*IEnumerable<LogDetails> listSuppressedLog = from suppressedLog in this.oDataContext.tblSuppressedNotifications.AsEnumerable()
                                                     where suppressedLog.chUsername == chUsername
                                                     select new LogDetails
                                                     {
                                                         eventId = suppressedLog.iEventId,
                                                         source = suppressedLog.vchSource
                                                     };*/
            //var result = this.oDataContext.tblSuppressedNotifications.Select(log => log.chUsername == chUsername);
            IEnumerable<tblSuppressedNotification> listSuppressedLog = this.oDataContext.RetrieveSuppressedList(chUsername).AsEnumerable();


            List<LogDetails> logs = new List<LogDetails>();
            foreach (tblSuppressedNotification table in listSuppressedLog)
            {
                LogDetails logRecord = new LogDetails();
                logRecord.eventId = table.iEventId;
                logRecord.source = table.vchSource;

                logs.Add(logRecord);
            }


            return logs;

        }

        /// <summary>
        ///     Modify existing user settings
        /// </summary>
        /// <param name="sUsername">User name for the user</param>
        /// <param name="sFirstname">User's first name</param>
        /// <param name="sLastname">User's last name</param>
        /// <param name="sPrimaryEmail">User's Primary e-mail address</param>
        /// <param name="sSecondaryEmail">User's Secondary e-mail address</param>
        /// <param name="sMachineNames">Collection of machines that user needs to monitor</param>
        /// <returns>True if modifying a new user successfully</returns>
        public Boolean modifyUserSetting(String sUsername,
                                         String sFirstname,
                                         String sLastname,
                                         String sPrimaryEmail,
                                         String sSecondaryEmail,
                                         String[] sMachineNames)
        {
            Boolean bSuccess;
            try
            {
                
               
                    var eUser = this.oDataContext.tblUsers.Single(aUser => aUser.chUsername == sUsername);

                    eUser.vchFirstname = sFirstname;
                    eUser.vchLastname = sLastname;
                    eUser.vchPrimaryEmail = sPrimaryEmail;
                    eUser.vchSecondaryEmail = sSecondaryEmail;

                    var qMonitoring = from monitor in this.oDataContext.tblMonitorings
                                      where monitor.chUsername == sUsername
                                      select monitor;

                    this.oDataContext.tblMonitorings.DeleteAllOnSubmit<tblMonitoring>(qMonitoring);

                    foreach (string sMachineName in sMachineNames)
                    {
                        tblMonitoring oMonitor = new tblMonitoring
                        {
                            iMonitoringId = 1,
                            chUsername = sUsername,
                            chMachineName = sMachineName.Trim(),
                            dtInserted = DateTime.Now

                        };


                        eUser.tblMonitorings.Add(oMonitor);
                        oMonitor = null;

                    }

                    this.oDataContext.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
               

                bSuccess = true;
            }
            catch (ChangeConflictException conflict)
            {
                throw new ChangeConflictException("Data has been previously updated", conflict);
                

            }

            catch (Exception oException)
            {
                using (ErrorLogger oLogger = new ErrorLogger())
                {
                    oLogger.dumpException(oException, this.GetType());
                }
                bSuccess = false;
            }
           
                return bSuccess;
            

        }

       /// <summary>
       ///      Retrieve a collection of users that monitoring a machine
       /// </summary>
       /// <param name="sMachineName">Machine name</param>
       /// <returns>return IEnumerable</returns>
        public IEnumerable<UserDetails> getUsers(String sMachineName, long iEventId)
        {
            List<UserDetails> userList;
            try
            {
                

               /* IEnumerable<UserDetails> enumerable = from monitoring in this.oDataContext.tblMonitorings
                                                      join aUser in this.oDataContext.tblUsers on monitoring.chUsername equals aUser.chUsername
                                                      where monitoring.chMachineName == sMachineName.Trim()
                                                      select new UserDetails
                                                      {
                                                          sUsername = aUser.chUsername,
                                                          sFirstname = aUser.vchFirstname,
                                                          sLastname = aUser.vchLastname,
                                                          sPrimaryEmail = aUser.vchPrimaryEmail,
                                                          sSecondaryEmail = aUser.vchSecondaryEmail

                                                      };*/
                using (ISingleResult<tblUser> users = this.oDataContext.RetrieveUserEmail(sMachineName, iEventId))
                {
                    userList = new List<UserDetails>();
                    foreach (tblUser user in users)
                    {
                        userList.Add(
                                new UserDetails
                                {
                                    sFirstname = user.vchFirstname,
                                    sLastname = user.vchLastname,
                                    sPrimaryEmail = user.vchPrimaryEmail,
                                    sSecondaryEmail = user.vchSecondaryEmail,
                                    sUsername = user.chUsername
                                }
                            );
                    }
                }

                return userList;
            }
            catch (Exception oException)
            {
                using (ErrorLogger oLogger = new ErrorLogger())
                {
                    oLogger.dumpException(oException, this.GetType());
                }

                return null;
            }
            


            
        }

        public IList<Machine> getMachines()
        {
            IList<Machine> machines = (from machine in this.oDataContext.tblMachines
                                       join tMachine in this.oDataContext.tblTypes on machine.iType equals tMachine.iTypeId
                                  select new Machine { machineName = machine.chMachineName,
                                   machineType = tMachine.vchType}).ToList<Machine>();

            return machines;
        }
        

        public UserDetails getUserDetails(String strUsername)
        {
            int counter = 0;
            tblUser user = this.oDataContext.tblUsers.SingleOrDefault(aUser => aUser.chUsername == strUsername);
            var monitoring = from monitor in this.oDataContext.tblMonitorings
                             where monitor.chUsername == strUsername
                             select monitor.chMachineName.Trim();

            if (user != null)
            {
                UserDetails userDetail = new UserDetails
                { 
                    sFirstname = user.vchFirstname,
                    sLastname = user.vchLastname,
                    sPrimaryEmail = user.vchPrimaryEmail,
                    sSecondaryEmail = user.vchSecondaryEmail
                };

                userDetail.machines = new String[monitoring.Count()];

                foreach (String monitor in monitoring)
                {
                    userDetail.machines[counter] = monitor.Trim();
                    counter++;
                }

                UserDetails returnData = userDetail;
                userDetail = null;

                return returnData;
            }
            else
            {
                

                return new UserDetails
                { 
                    sFirstname = "",
                    sLastname = "",
                    sPrimaryEmail = "",
                    sSecondaryEmail = ""
                };
            }
        }


        public void Dispose()
        {
            this.oDataContext = null;
        }
    }
}
