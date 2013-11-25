﻿using System;
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
using System.Text;

namespace AIOPClient.Controllers
{
    public class BookingStatusController : Controller
    {
        //
        // GET: /BookingStatus/

        public ActionResult Index()
        {
            AIOPClient.Models.UserSession session = null;
            session = AIOPClient.Models.UserSession.GetInstance();
            if (!session.logged)
            {
                return Redirect("../Home/Index");
            }
            else
            {
                getReservationList();
                return View();
            }
        }

        public bool getReservationList()
        {
            //Api's url building
            AIOPClient.Models.UserSession session = null;
            session = AIOPClient.Models.UserSession.GetInstance();
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/planning/display?id_teacher=";
            String idUser=session.id_user.ToString();
            Debug.WriteLine(idUser);
            urlApi += idUser;

            //Deserialization of the returned json
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(urlApi);
                JArray jsonVal = JArray.Parse(result) as JArray;
                dynamic reservations = jsonVal;
                ViewBag.reservationsList = reservations;
                return true;
            }
        }

        [HttpPost]
        public ActionResult deleteReservation(int id)
        {
            Debug.WriteLine("Ninja !");
            Debug.WriteLine(id);
            //Api's url building
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/planning/Delete?id_booking=" + id.ToString();

            using (var client = new WebClient())
            {
                var result = client.DownloadString(urlApi);
                //If return is null then the reservations wasn't refused properly
                if (result == null)
                    return View();
            }
            return RedirectToAction("Index", "BookingStatus");
        }
    }
}
