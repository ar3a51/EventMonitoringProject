using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.Logging;
using DataLayer.Model;

namespace DataLayer
{
    
    /// <summary>
    ///     Provide data access to log the event viewer entry to database.
    /// </summary>
    /// <author>Hanggara Prameswara</author>
    /// <date>05/07/2011</date>
    public class EventLogDataLayer : IDisposable
    {
        DataConDataContext oDataContext = null;




        public EventLogDataLayer(String connectionString)
        {
            this.oDataContext = new DataConDataContext(connectionString);
        }

        /// <summary>
        ///  Insert event details to db
        /// </summary>
        /// <param name="sEventType">Machine type (db, biztalk, subs)</param>
        /// <param name="sEventMessage">event value</param>
        /// <param name="sMachineName">name of the machine</param>
        /// <param name="sUserName">User name that generates the event</param>
        /// <param name="dtTimeGenerated">Time that the event triggered</param>
        /// <returns>True if successfully inserted</returns>
        public Boolean insertLogData( long eventId,
                                    String sEventType, 
                                    String sEventMessage, 
                                    String sMachineName, 
                                    String sUserName,
                                    String strSource,
                                    DateTime dtTimeGenerated)
        {

            try
            {
               
                    tblEventLog eventLog = new tblEventLog
                    {
                        iEventId = eventId,
                        chUsername = sUserName.Trim(),
                        chMachineName = sMachineName.Trim(),
                        dtTimeGenerated = dtTimeGenerated,
                        vchSource   =   strSource,
                        vchEventMessage = sEventMessage.Trim(),
                        vchEventType = sEventType.Trim(),

                    };


                    this.oDataContext.tblEventLogs.InsertOnSubmit(eventLog);

                    this.oDataContext.SubmitChanges();

                    eventLog = null;
                
                    return true;

            }

            catch (Exception oException)
            {
                using (ErrorLogger oLogger = new ErrorLogger())
                {
                    oLogger.dumpException(oException, this.GetType());
                }
                return false;
                //throw new Exception(oException.Message + " " + oException.ToString());
            }

 
        
     
            

            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMachineName"></param>
        /// <returns></returns>
        public IEnumerable<LogDetails> getLogData(String sMachineName)
        {

            //return this.oDataContext.tblEventLogs.Where(chMachineName => sMachineName).ToArray();
            IEnumerable<LogDetails> query = from events in this.oDataContext.tblEventLogs
                                            where events.chMachineName.Equals(sMachineName.Trim())
                                            select new LogDetails
                                            {
                                                eventId = (int)events.iEventId,
                                                source = events.vchSource,
                                                chUsername = events.chUsername,
                                                dtGenerated = (DateTime)events.dtTimeGenerated,
                                                type = events.vchEventType

                                            };

            return query;



        }
        /// <summary>
        ///     Add new machine details to database
        /// </summary>
        /// <param name="sType">Machine type (db, biztalk, subs)</param>
        /// <param name="sMachineName">name of the machine</param>
        /// <param name="sEnvironment">Environment where the machine sits</param>
        /// <returns>True if adding new machine successfully</returns>
        public Boolean addNewMachine(String sType, String sMachineName, String sEnvironment)
        {
            try
            {


                var machine = this.oDataContext.tblMachines.SingleOrDefault(name => name.chMachineName == sMachineName);

                if (machine != null)
                    return true;
                
                    var anEntity = this.oDataContext.tblTypes.SingleOrDefault(type => type.vchType == sType);

                    if (anEntity != null)
                    {

                        tblMachine oMachine = new tblMachine
                        {
                            chMachineName = sMachineName.Trim(),
                            dtAdded = DateTime.Now,
                            vchEnvironment = sEnvironment.Trim(),
                            iType = anEntity.iTypeId
                        };

                        this.oDataContext.tblMachines.InsertOnSubmit(oMachine);

                        oMachine = null;
                        

                    }
                    else
                    {
                        tblType oMachineType = new tblType
                        {
                            dtInsertDate = DateTime.Now,
                            vchType = sType.Trim()
                        };



                        tblMachine oMachine = new tblMachine
                        {
                            chMachineName = sMachineName.Trim(),
                            dtAdded = DateTime.Now,
                            vchEnvironment = sEnvironment.Trim()
                        };

                        oMachineType.tblMachines.Add(oMachine);
                        this.oDataContext.tblTypes.InsertOnSubmit(oMachineType);

                        oMachine = null;
                        oMachineType = null;
                    }

                    
                    this.oDataContext.SubmitChanges();

                   
                

                return true;
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

        public Boolean removeMachine(String strMachineName)
        {

            try
            {
                var machine = from aMachine in this.oDataContext.tblMachines
                              where aMachine.chMachineName.Contains(strMachineName)
                              select aMachine;

                var monitoring = from monitor in this.oDataContext.tblMonitorings
                                 where monitor.chMachineName.Contains(strMachineName)
                                 select monitor;

                this.oDataContext.tblMachines.DeleteAllOnSubmit<tblMachine>(machine);

                this.oDataContext.tblMonitorings.DeleteAllOnSubmit<tblMonitoring>(monitoring);
                

                this.oDataContext.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                              
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

    



        public void Dispose()
        {

            
            
            this.oDataContext = null;

        }
    }
}
