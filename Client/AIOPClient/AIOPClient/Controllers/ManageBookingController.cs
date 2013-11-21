using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;
using System.Text;

namespace AIOPClient.Controllers
{
    public class ManageBookingController : Controller
    {
        //
        // GET: /ManageBooking/

        public ActionResult Index()
        {
            UserSession session = null;
            session = UserSession.GetInstance();
            if (!session.logged)
            {
                return Redirect("../Home/Index");
            }
            else if (!session.admin)
            {
                return Redirect("../MyCalendar/Index");
            }
            else
            {
                getWaitingList();
                return View();
            }
        }

        public bool getWaitingList()
        {
            ////Api's url building
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/admin/waiting";
            //Deserialization of the returned json
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(urlApi);
                JArray jsonVal = JArray.Parse(result) as JArray;
                dynamic waitings = jsonVal;
                ViewBag.waitingsList = waitings;
                return true;
            }
        }

        [HttpPost]
        public bool refuseReservation(int id)
        {
            Debug.WriteLine(id);
            //Api's url building
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/admin/Refuse?Id_Booking=" + id.ToString();

            using (var client = new WebClient())
            {
                var result = client.DownloadString(urlApi);
                //If return is null then the reservations wasn't refused properly
                if (result == null)
                    return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult acceptReservation(int id)
        {
            Debug.WriteLine(id);
            //Api's url building
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/admin/Accept?Id_Booking=" + id.ToString();

            using (var client = new WebClient())
            {
                var result = client.DownloadString(urlApi);
                //If return is null then the reservations wasn't accepted properly
                if (result == null)
                    return View();
            }
            return View();
        }
    }
}
