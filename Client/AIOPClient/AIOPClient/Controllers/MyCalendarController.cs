using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;

namespace AIOPClient.Controllers
{
    public class MyCalendarController : Controller
    {
        //
        // GET: /MyCalendar/

        public ActionResult Index()
        {
            UserSession session = null;
            session = UserSession.GetInstance();
            if (!session.logged)
            {
                return Redirect("../Home/Index");
            }
            else
                return View();
        }

        [ActionName("GetEvents")]
        public ActionResult GetEvents()
        {
            JObject jo = new JObject();

        //    var events = new[]
        //{     
        //    new { title = "test", start = "2013-11-19T14:08:00Z", end = "2013-11-19T16:09:00Z", editable = false },
        //    new { title = "test", start = "2013-11-19 16:08:00 ", end = "2013-11-19 18:09:00", editable = false },
        //    new { title = "test", start = "2013-11-20", end = "2013-11-20", editable = false }
        //};

        //    var events2 = new[]
        //{     
        //    new { title = "test", start = "2013-11-21T14:08:00Z", end = "2013-11-21T16:09:00Z", editable = false },
        //    new { title = "test", start = "2013-11-22", end = "2013-11-22", editable = false }
        //};

        //    var events3 = events.Concat(events2);

            using (var client = new WebClient())
            {
                var result = client.DownloadString("http://162.38.113.204/api/planning/display?group_name=IG4");
                JArray jsonArray = JArray.Parse(result) as JArray;
               // dynamic reservations = jsonVal;

                var eventsJ = new[]
                    {
                        new { title = "test", start = "2013-11-19T14:08:00Z", end = "2013-11-19T16:09:00Z", editable = false }
                    };
                    
 
                foreach (dynamic reservation in jsonArray)
                {
                    String state = reservation.State;
                    String start = reservation.Start_Date;
                    DateTime dateStart = Convert.ToDateTime(start);
                    String startString = dateStart.ToString("yyyy-dd-MM HH:mm:ss");

                    String end = reservation.End_Date;
                    DateTime dateEnd = Convert.ToDateTime(end);
                    String endString = dateEnd.ToString("yyyy-dd-MM HH:mm:ss");

                    var eventReservation = new[]
                    {
                        new { title = state, start = startString, end = endString, editable = false }
                    };

                    //eventsJ = eventsJ.Concat(eventReservation);
                }


                //dynamic json1 = jsonArray[0];
                //String state = json1.State;
                //String start = json1.Start_Date;
                //DateTime dateStart = Convert.ToDateTime(start);
                //String startString = dateStart.ToString("yyyy-dd-MM HH:mm:ss");

                //String end = json1.End_Date;
                //DateTime dateEnd = Convert.ToDateTime(end);
                //String endString = dateEnd.ToString("yyyy-dd-MM HH:mm:ss");

        //        var eventsJson = new[]
        //{     
        //    new { title = "test", start = "2013-11-19T14:08:00Z", end = "2013-11-19T16:09:00Z", editable = false },
        //    new { title = "test", start = "2013-11-19 16:08:00 ", end = "2013-11-19 18:09:00", editable = false },
        //    new { title = state, start = startString, end = endString, editable = false }
        //};
                
                return Json(eventsJ, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
