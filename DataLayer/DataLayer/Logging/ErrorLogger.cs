using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace DataLayer.Logging
{
    
    /// <summary>
    ///     Dump any exception thrown and put it in the text file.
    /// </summary>
    /// <author>Hanggara Prameswara</author>
    /// <date>05/07/2011</date>
    class ErrorLogger : IDisposable
    {
        private String sFilename;

        public ErrorLogger()
        {
            //TODO: if Necessary, change the error logger to write to event log.
            this.sFilename = @"D:\ErrorLog.txt"; 
        }

        /// <summary>
        ///     write the exception info to text file
        /// </summary>
        /// <param name="oException">Exception object in which information will be dumped to text file</param>
        /// <param name="oCurrentObject">The assembly where the exception is thrown</param>
        public void dumpException(Exception oException, Type oCurrentObject)
        {
            using (StreamWriter oWriter = new StreamWriter(this.sFilename,true))
            {
                oWriter.WriteLine("Exception: " + oException.ToString());
                oWriter.WriteLine("Assembly name: " + oCurrentObject.AssemblyQualifiedName);
                oWriter.WriteLine("Stacktrace: " + oException.StackTrace);
                oWriter.WriteLine("Error Source: " + oException.Source);
                oWriter.WriteLine("Error Message: " + oException.Message);
                oWriter.WriteLine("Time Generated: " + DateTime.Now.ToString());
                oWriter.WriteLine("    ");
                oWriter.Close();

            }

        }





        void IDisposable.Dispose()
        {
            //do nothing
        }
    }
}
