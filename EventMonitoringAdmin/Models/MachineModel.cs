using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventMonitoringAdmin.UserManagement;
using System.Net;

namespace EventMonitoringAdmin.Models
{
    public class MachineModel
    {
        private User userClient;

        public MachineModel()
        {
            this.userClient = new User();
            this.userClient.Credentials = CredentialCache.DefaultCredentials;
           
        }

        public List<SelectListItem> retrievingMachineList()
        {
            ResultMachines resultSet = this.userClient.getMachines();

            return resultSet.machines.Select(machine => new SelectListItem()
                                                            {
                                                                Value = machine.machineName.Trim(), Text = machine.machineName.Trim()
                                                            }).ToList();

        }
    }
}