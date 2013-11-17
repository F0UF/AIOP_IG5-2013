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
            statusListModel model = new statusListModel();
            using (var client = new WebClient())
            {
                var json = client.DownloadString("http://aiopninjaserver.no-ip.biz/api/planning/display?id_teacher=11");

                if (json.Equals(""))
                {
                    return false;
                }
                else
                {
                    JObject o = JObject.Parse(json);
                    model.dateBegin = (string)o["Start_Date"];
                    model.dateEnd = (string)o["End_Date"];
                    model.teaching = (string)o["Module_Name"];
                    model.promotion = (string)o["Group_Name"];
                    model.computers = (string)o["Computer"];
                    model.projector = (string)o["Projector"];
                    model.status = (string)o["State"];
                    Console.WriteLine(model.dateBegin);
                    Console.WriteLine(model.dateEnd);
                    Console.WriteLine(model.teaching);
                    Console.WriteLine(model.promotion);
                    Console.WriteLine(model.computers);
                    Console.WriteLine(model.projector);
                    Console.WriteLine(model.status);
                    return true;
                }
            }
        }
    }
}
