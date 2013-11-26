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
        public ActionResult GetEvents(int id_teacher)
        {

            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString("http://162.38.113.204/api/planning/display?id_teacher=" + id_teacher);
                JArray jsonArray = JArray.Parse(result) as JArray;

                var eventList = new List<object>();

                foreach (dynamic reservation in jsonArray)
                {
                    String title = reservation.Teaching.Course.Subject.Subject_Name;
                    String start = reservation.Start_Date;
                    DateTime dateStart = DateTime.ParseExact(start, "MM/dd/yyyy HH:mm:ss", null);
                    String startString = dateStart.ToString("yyyy-MM-dd HH:mm:ss");

                    String end = reservation.End_Date;
                    DateTime dateEnd = DateTime.ParseExact(end, "MM/dd/yyyy HH:mm:ss", null);
                    String endString = dateEnd.ToString("yyyy-MM-dd HH:mm:ss");

                    String teacher = reservation.Teaching.Teacher.First_Name + " " + reservation.Teaching.Teacher.Last_Name;
                    String salle = reservation.Room.Room_Number;
                    String group = reservation.Teaching.Group.Group_Name;
                    String time_slot = dateStart.ToString("HH:mm") + " - " + dateEnd.ToString("HH:mm");

                    String state = reservation.State;
                    String color = "";
                    if (state.Equals("Validé"))
                    {
                        color = "#487CB0";
                    }
                    if (state.Equals("En attente"))
                    {
                        color = "#E8B320";
                    }
                    if (state.Equals("Refusé"))
                    {
                        color = "#A00000";
                    }
                    eventList.Add(new { title = title, start = startString, end = endString, allday = false, teacher = teacher, salle = salle, group = group, time_slot = time_slot, color = color, state = state });
                }


                return Json(eventList.ToArray(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
