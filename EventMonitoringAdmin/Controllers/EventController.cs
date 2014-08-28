using System;
using System.Web.Mvc;
using EventMonitoringAdmin.Models;

namespace EventMonitoringAdmin.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        private EventModel _evModel;

        public EventController()
        {
            _evModel = new EventModel();
        }
        public ActionResult GetEventList(String machineName)
        {
            var eventDetail = _evModel.getEventLogList(machineName);
            return View("EventList",eventDetail.eventList);
        }

    }
}
