using System;
using System.Data;
using System.Web.Mvc;
using EventMonitoringAdmin.Models;
using EventMonitoringAdmin.UserManagement;

namespace EventMonitoringAdmin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private MachineModel machine;
        private UserModel _user;

        public UserController()
        {
            _user = new UserModel();
            machine = new MachineModel();
            ViewData["username"] = Environment.UserDomainName + @"\" + Environment.UserName;
            ViewData["selections"] = machine.retrievingMachineList();
            machine = null;
            ViewData["myDetails"] = Environment.UserDomainName + @"\" +Environment.UserName;
                

        }

        public ActionResult Index()
        {
            UserDetails user = _user.getUserDetails(ViewData["username"].ToString());
            if (user.machines == null)
             return   RedirectToAction("RegisterNewUser");

            if (user.machines[0].ToLower() == "null")
                return RedirectToAction("editUser");
            
            return View(user);
        }

        public ActionResult RegisterNewUser()
        {
            var user = new UserModel();
            UserDetails credentials = user.getUserDetailsFromAD(Environment.UserName);

            ViewData["username"] = Environment.UserDomainName + @"\" + credentials.sUsername;
            ViewData["firstName"] = credentials.sFirstname;
            ViewData["lastName"] = credentials.sLastname;
            ViewData["mail"] = credentials.sPrimaryEmail;
            //ViewData["username"] = Environment.UserDomainName + "\\" + Environment.UserName;
            

            return View();
        }

        public ActionResult editUser()
        {
            
            return View("EditUser");
        }

        [HttpPost]
        public JsonResult Add(UserModel user)
        {
            
           return Json(user.AddUser());

            //return View("EditUser").to

        }

        [HttpPost]
        public ActionResult saveSuppressedLog(String operation, LogDetails[] log)
        {
            //  try
          //  {

            DataTable result = this._user.saveSuppressedLog(operation, ViewData["username"].ToString(), log);

            return View("../Event/EventList", result);
           /* }
            catch (Exception ex)
            {
                return ex.Message;

            }*/
        }

        public ActionResult getSuppressedLog()
        {
            DataTable result = this._user.getSuppressedLogs(ViewData["username"].ToString());

            return View("../Event/EventList", result);
        }

        [HttpPost]
        public ActionResult deleteLog(LogDetails[] log)
        {
            DataTable result = this._user.deleteSuppressedLog(ViewData["username"].ToString(), log);

            return View("../Event/EventList", result);
        }

        [HttpPost]
        public JsonResult modify(UserModel user)
        {
            return Json(user.modifyUser());
        }

        [HttpPost]
        public JsonResult getUser(UserModel user)
        {
            return Json(user.getUserDetails(user.userName));
        }

    }
}
