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
using System.Collections.Specialized;


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
        public ActionResult Index(String promo, String year, String group, String subject, String type, String projector, String computers, String capacity, String date, String start, String end)
        {
            using (var client = new WebClient())
            {
                UserSession session = null;
                session = UserSession.GetInstance();
                client.Encoding = Encoding.UTF8;
                string realName = promo + year + " " + group;

                string start_time = date + " " + start;
                string end_time = date + " " + end;

                JObject json = new JObject();
                json.Add("Id_Teacher", session.id_user);

                if (capacity == "Small(-30)")
                    json.Add("Capacity", 30);
                else if (capacity == "Medium (between 30 and 50)")
                    json.Add("Capacity", 50);
                else if (capacity == "Large (between 50 and 70)")
                    json.Add("Capacity", 70);
                else json.Add("Capacity", 100);

                json.Add("Group_Name", realName);
                json.Add("Subject_Name", subject);
                json.Add("Type", type);

                if (projector == null)
                    json.Add("Projector", false);
                else
                    json.Add("Projector", true);
                if (computers == null)
                    json.Add("Computer", false);
                else
                    json.Add("Computer", true);
                json.Add("Start_At", start_time);
                json.Add("End_At", end_time);

                String urlApi = "http://localhost:55080/api/planning/CreateBooking";
                Debug.Write(json.ToString());

                var response = client.UploadString(urlApi, "POST", json.ToString());

                if (response.Equals("null"))
                {
                    return Content("<script> alert(\'No room available\');location.href='./';</script>");
                }
                else
                {
                    return Content("<script> alert(\'Room booked\');location.href='./';</script>");
                }
            }

            //using (var client = new WebClient())
            //{
            //    UserSession session = null;
            //    session = UserSession.GetInstance();
            //    client.Encoding = Encoding.UTF8;
            //    string realName = promo + year + " " + group;

            //    string boolproj;
            //    string boolcomp;
            //    int cap;

            //    if (projector == null)
            //    {
            //        boolproj = "false";
            //    }
            //    else boolproj = "true";

            //    if (computers == null)
            //    {
            //        boolcomp = "false";
            //    }
            //    else boolcomp = "true";

            //    if (capacity == "Small(-30)")
            //        cap = 30;
            //    else if (capacity == "Medium (between 30 and 50)")
            //        cap = 50;
            //    else if (capacity == "Large (between 50 and 70)")
            //        cap = 70;
            //    else cap = 100;

            //    string start_time = date + " " + start;
            //    string end_time = date + " " + end;

            //    string test = "http://aiopninjaserver.no-ip.biz/api/planning/CreateBooking?Id_Teacher=" + session.id_user + "&Group_Name=" + realName + "&Subject_Name=" + subject + "&Type=" + type + "&Projector=" + boolproj + "&Computer=" + boolcomp + "&Capacity=" + cap + "&End_Time=" + end_time + "&Start_Time=" + start_time;
            //    var result = client.DownloadString(test);

            //    if (result.Equals("null"))
            //    {
            //        return Content("<script> alert(\'No room available\');location.href='./';</script>" );
            //    }
            //    else
            //    {
            //        return Content("<script> alert(\'Room booked\');location.href='./';</script>");
            //    }
            //}
        }

        /*[HttpPost]
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
                }
            }
        }*/

    }
}
