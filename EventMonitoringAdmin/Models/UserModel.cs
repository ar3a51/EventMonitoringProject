using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.DirectoryServices;
using System.Net;
using EventMonitoringAdmin.UserManagement;

namespace EventMonitoringAdmin.Models
{
    public class UserModel : IDisposable
    {
        private User userClient;
        public String userName { set; get; }
        public String firstName { set; get; }
        public String lastName { set; get; }
        public String primaryEmail { set; get; }
        public String secondaryEmail { set; get; }
        public String[] machineCollections { set; get; }
        
        public UserModel()
        {
            //this.userClient = new User();
            
            
        }

        public ResultSet AddUser()
        {
            ResultSet result;

            using (userClient = new User())
            {
                userClient.Credentials = CredentialCache.DefaultCredentials;
                result = userClient.addNewUser(userName,
                                            firstName,
                                            lastName,
                                            primaryEmail,
                                            secondaryEmail,
                                            machineCollections);
            }

            if (result.stringResultType == "success")
            {
                result.stringResultMessage = "<img src='" + VirtualPathUtility.ToAbsolute("~/Theme/Images/check.gif") + "' class='image' />&nbsp;" + result.stringResultType;
            }

            return result;

        }

        public ResultSet modifyUser()
        {
            ResultSet result;

            using (userClient = new User())
            {
                userClient.Credentials = CredentialCache.DefaultCredentials;
                result = userClient.modifyUserSettings(userName,
                                            firstName,
                                            lastName,
                                            primaryEmail,
                                            secondaryEmail,
                                            machineCollections);
            }

            if (result.stringResultType == "success")
            {
                result.stringResultMessage = "<img src='" + VirtualPathUtility.ToAbsolute("~/Theme/Images/check.gif") + "' class='image' />&nbsp;" + result.stringResultType;
            }
            
            return result;
        }

        public UserDetails getUserDetails(String strUsername)
        {
            UserDetails result;
            using (userClient = new User())
            {
                userClient.Credentials = CredentialCache.DefaultCredentials;
                UserDetails user = userClient.getUserDetails(strUsername);
                result = user;
            }
            

            return result;
        }

        public UserDetails getUserDetailsFromAD(String userLogin)
        {
            var entry = new DirectoryEntry("LDAP://CA");
            var dsearch = new DirectorySearcher(entry)
                              {Filter = "(&(objectClass=user)(SAMAccountname=" + userLogin + "))"};

            //dSearch..Filter = "(&(objectClass=user)(l=" + Name + "))";

            var result = dsearch.FindOne();

            var user = new UserDetails
            {
                sFirstname = GetProperty(result, "GivenName"),
                sLastname = GetProperty(result, "sn"),
                sPrimaryEmail = GetProperty(result, "Mail"),
                sUsername = userLogin
            };



            UserDetails resultDetail = user;
            user = null;
            //foreach (SearchResult sResultSet in dsearch.FindOne())

            return resultDetail;
        }

        public DataTable saveSuppressedLog(String operation, String chUsername, LogDetails[] log)
        {
            
               // DataTable resultTable;
                resultUser result;
                using (userClient = new User())
                {
                    result = userClient.addSuppressedLog(operation, chUsername, log);
                    if (result.stringResultType == "fail")
                        throw new Exception(result.stringResultMessage);
                }

                return result.suppressedLog;
            
           
        }

        public DataTable getSuppressedLogs(String username)
        {

            resultUser result;
            using (userClient = new User())
            {
                result = userClient.getAllSuppressedLog(username);
                if (result.stringResultType == "fail")
                    throw new Exception(result.stringResultMessage);
            }

            return result.suppressedLog;
        }

        public DataTable deleteSuppressedLog(String username, LogDetails[] log)
        {
            resultUser result;
            using (userClient = new User())
            {
                result = userClient.deleteSuppressedLog(username, log);
                if (result.stringResultType == "fail")
                    throw new Exception(result.stringResultMessage);
            }

            return result.suppressedLog;
        }

       private String GetProperty(SearchResult searchResult, string PropertyName)
       {
           if(searchResult.Properties.Contains(PropertyName))
               {
                return searchResult.Properties[PropertyName][0].ToString() ;
               }
           return " ";
       }


        public void  Dispose()
        {
            userClient = null;
        }
}
}