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
        public JArray GetEvents(double start, double end)
        {

            JArray ja = new JArray();

            JObject jo = new JObject();
            jo.Add("title", "Objective-C");
            jo.Add("start", "2013-12-19 15:30:00");
            jo.Add("end", "2013-12-19 17:30:00");
            jo.Add("allDay", false);
            ja.Add(jo);

            jo = new JObject();
            jo.Add("title", "Reseaux");
            jo.Add("start", "2013-12-18 12:30:00");
            jo.Add("end", "2013-12-18 14:30:00");
            jo.Add("allDay", false);
            ja.Add(jo);

            return ja ;
        }
    }
}
