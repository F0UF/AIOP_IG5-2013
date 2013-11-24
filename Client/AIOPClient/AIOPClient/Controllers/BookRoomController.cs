using AIOPClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Newtonsoft.Json.Linq;


namespace AIOPClient.Controllers
{
    public class BookRoomController : Controller
    {
        //
        // GET: /BookClass/

        public ActionResult Index()
        {
            UserSession session = null;
            session = UserSession.GetInstance();
            if (!session.logged)
            {
                return Redirect("../Home/Index");
            }
            else
            {
                using (var client = new WebClient())
                
                {
                    client.Encoding = Encoding.UTF8;
                    var result = client.DownloadString("http://aiopninjaserver.no-ip.biz/api/subject/getsubjects");
                    JArray jsonVal = JArray.Parse(result) as JArray;
                    dynamic subjects = jsonVal;
                    ViewBag.SubjectList = subjects;
                    return View();
                }
             
            }
        }

        //
        // GET: /BookClass/

        [HttpPost]
        public ActionResult Index(String promo, String year, String group, String subject, String type, String projector, String computers, String date, String start, String end)
        {
         return View();   
        }

        [HttpPost]
        public ActionResult SubmitBooking (int id)
        {
            Debug.WriteLine(id);
            //Api's url building
            String urlApi = "http://aiopninjaserver.no-ip.biz/api/planning/CreateBooking?json=";

            using (var client = new WebClient())
            {
                var result = client.DownloadString(urlApi);
                //If return is null then the reservations wasn't accepted properly
                if (result == null)
                    return View();
            }
            return View();
        }

        private bool IsBookingOK(UserModel model)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                var result = client.UploadString("http://aiopninjaserver.no-ip.biz/api/planning/CreateBooking", "POST", json);
                if (result == null)
                    return false;
                else return true;
               /* JObject jresult = JObject.Parse(result);
                dynamic jo = jresult;

                UserSession session = null;
                session = UserSession.GetInstance();

               // errorMsg = jo.Message;

                if (jo.Status == 0)
                {
                    return false;
                }
                else
                {
                    session.logged = true;
                    session.userName = jo.First_Name + " " + jo.Last_Name;
                    session.admin = jo.Super_User;
                    session.id_user = jo.Id_User;
                    return true;
                }*/
            }
        }

    }
}
