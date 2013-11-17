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
            using (var client = new WebClient())
            {
                var result = client.DownloadString("http://aiopninjaserver.no-ip.biz/api/planning/display?id_teacher=11");
                JArray jsonVal = JArray.Parse(result) as JArray;
                dynamic reservations = jsonVal;

                foreach (dynamic reservation in reservations)
                {
                    ViewBag.test=reservation.Start_Date;
                    /**foreach (dynamic song in reservation.Room)
                    {
                        Console.WriteLine("\t" + song.SongName);
                    }**/
                }

                //Debug.WriteLine(reservations[0].Start_Date);
                /**Debug.WriteLine(reservations[0].Songs[1].SongName);

                string dateBegin = j.Start_Date;
                string dateEnd = j.End_Date;
                string teaching = j.Module_Name;
                string promotion = j.Groupe_Name;
                string computers = j.Computer;
                string projector = j.Projector;
                string status = j.State;**/

                return true;
            }
        }
    }
}
