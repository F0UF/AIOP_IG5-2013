using Login.Models;
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

namespace Login.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        public ActionResult Login(string url)
        {
            ViewBag.Url = url;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string url)
        {
            if (ModelState.IsValid && IsLoginOK(model))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Fucking wrong password madafaka.");
            return View(model);
        }

        private bool IsLoginOK(LoginModel model)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(model);
                var result = client.UploadString("http://localhost:55450/api/account", "POST", json);

                if (result.Equals("\"ok\""))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
