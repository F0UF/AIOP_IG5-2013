using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AIOPClient.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AIOPClient.Controllers
{
    public class BookingStatusController : Controller
    {
        //
        // GET: /BookingStatus/

        public ActionResult Index()
        {
            getReservationList();
            return View();
        }

        public bool getReservationList()
        {
            //Construction de l'url de l'API à appeller 
            AIOPClient.Models.UserSession session = null;
            session = AIOPClient.Models.UserSession.GetInstance();
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/planning/display?id_teacher=";
            String idUser=session.id_user.ToString();
            urlApi += idUser;
            using (var client = new WebClient())
            {
                var result = client.DownloadString(urlApi);
                JArray jsonVal = JArray.Parse(result) as JArray;
                dynamic reservations = jsonVal;
                ViewBag.reservationsList = reservations;
                return true;
            }
        }
    }
}
