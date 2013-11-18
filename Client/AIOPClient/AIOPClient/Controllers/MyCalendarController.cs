using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIOPClient.Controllers
{
    public class MyCalendarController : Controller
    {
        //
        // GET: /MyCalendar/

        public ActionResult Index()
        {
            return View();
        }

        [ActionName("GetEvents")]
        public ActionResult GetEvents()
        {

            //JArray ja = new JArray();

            //JObject jo = new JObject();
            //jo.Add("title", "Objective-C");
            //jo.Add("start", "2013-12-19 15:30:00");
            //jo.Add("end", "2013-12-19 17:30:00");
            //jo.Add("allDay", false);
            //ja.Add(jo);

            //jo = new JObject();
            //jo.Add("title", "Reseaux");
            //jo.Add("start", "2013-12-18 12:30:00");
            //jo.Add("end", "2013-12-18 14:30:00");
            //jo.Add("allDay", false);
            //ja.Add(jo);

            //return ja ;

            var events = new[]
        {     
            new { title = "test", start = "2013-11-19T14:08:00Z", end = "2013-11-19T16:09:00Z", editable = false },
            new { title = "test", start = "2013-11-19 16:08:00 ", end = "2013-11-19 18:09:00", editable = false },
            new { title = "test", start = "2013-11-20", end = "2013-11-20", editable = false }
        };
            return Json(events, JsonRequestBehavior.AllowGet);
        }
    }
}
