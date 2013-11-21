using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;
using System.Text;

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

            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString("http://162.38.113.204/api/planning/display?group_name=IG4");
                JArray jsonArray = JArray.Parse(result) as JArray;

                var eventList = new List<object>();

                foreach (dynamic reservation in jsonArray)
                {
                    String title = reservation.Teaching.Course.Subject.Subject_Name;
                    String start = reservation.Start_Date;
                    DateTime dateStart = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm:ss",null);
                    String startString = dateStart.ToString("yyyy-MM-dd HH:mm:ss");

                    String end = reservation.End_Date;
                    DateTime dateEnd = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm:ss", null);
                    String endString = dateEnd.ToString("yyyy-MM-dd HH:mm:ss");

                    eventList.Add(new { title = title, start = startString, end = endString, editable = false });
                }

                
                return Json(eventList.ToArray(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
