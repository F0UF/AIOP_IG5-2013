using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AIOPClient.Controllers
{
    public class WorkingHoursController : Controller
    {
        //
        // GET: /WorkingHours/

        public ActionResult Index()
        {
            UserSession session = null;
            session = UserSession.GetInstance();
            if (!session.logged)
            {
                return Redirect("../Home/Index");
            }
            else
                getWorkingHours();
                return View();
        }

        

        public bool getWorkingHours()
        {
            //Api's url building
            AIOPClient.Models.UserSession session = null;
            session = AIOPClient.Models.UserSession.GetInstance();
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/teacher/summaryHours?id_teacher=";
            //String urlApi = "http://localhost:55080/api/teacher/summaryHours?id_teacher=";
            String idUser = session.id_user.ToString();
            urlApi += idUser;
            Console.Write(urlApi);

            //Deserialization of the returned json
            using (var client = new WebClient())
            {
                var result = client.DownloadString(urlApi);
                Console.Write(result);
                JObject jsonVal = JObject.Parse(result) as JObject;
                dynamic hours = jsonVal;
                ViewBag.hoursList = hours;
                return true;
            }
        }


    }
}
