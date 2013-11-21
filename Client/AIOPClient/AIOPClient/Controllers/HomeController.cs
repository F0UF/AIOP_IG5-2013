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


namespace AIOPClient.Controllers
{
    public class HomeController : Controller
    {
        String errorMsg;
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Home/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel model)
        {
            if (ModelState.IsValid && IsLoginOK(model))
            {
                return RedirectToAction("Index", "MyCalendar");
            }
            else
            {
                ModelState.AddModelError("", "");
                return View();
                //return Content("<script> window.alert(\'" + errorMsg + "\')</script>;<script language=\"JavaScript\">window.location.href='../Home/Index'</script>;");
            }
        }

        public ActionResult LogOut()
        {
            UserSession.DeleteInstance();
            return RedirectToAction("Index", "Home");
        }

        private bool IsLoginOK(UserModel model)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                var result = client.UploadString("http://aiopninjaserver.no-ip.biz/api/account/login", "POST", json);

                JObject jresult = JObject.Parse(result);
                dynamic jo = jresult;

                UserSession session = null;
                session = UserSession.GetInstance();

                session.userName = jo.First_Name + " " + jo.Last_Name;
                session.admin = jo.Super_User;
                session.id_user = jo.Id_User;

                errorMsg = jo.Message;

                if (jo.Status == 0)
                {
                    return false;
                }
                else
                {
                    session.logged = true;
                    return true;
                }
            }
        }

    }
}
