using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIOPServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AIOPServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(String message)
        {
            ViewBag.AlertMessage = message;
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string mdp)
        {
            dynamic jo = isLoginOk(username, mdp);
            String message = jo.Message;

            string script = "<script language='javascript' type='text/javascript'>";
            script += "alert('" + message + "');";

            if (jo.id_user > 0)
                script += "location.href='http://localhost:55080/';";
            else
                script += "location.href='http://localhost:55080/home/login';";

            script += "</script>";
            return Content(script);


        }


        public dynamic isLoginOk(string username, string mdp)
        {

            //    string postData = "username=tom";

            //    // create the POST request
            //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:55080/api/account/login");
            //    webRequest.Method = "POST";
            //    webRequest.ContentType = "application/x-www-form-urlencoded";
            //    webRequest.ContentLength = postData.Length;

            //    // POST the data
            //    using (StreamWriter requestWriter2 = new StreamWriter(webRequest.GetRequestStream()))
            //    {
            //        requestWriter2.Write(postData);
            //    }

            //    //  This actually does the request and gets the response back
            //    HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();

            //    string responseData = string.Empty;

            //    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            //    {
            //        // dumps the HTML from the response into a string variable
            //        responseData = responseReader.ReadToEnd();
            //    }

            //    return responseData;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                JObject jo = new JObject();
                jo.Add("username", username);
                jo.Add("password", mdp);
                string json = jo.ToString();

                var result = client.UploadString("http://localhost:55080/api/account/login", "POST", json);

                return JObject.Parse(result);
            }
        }
    }
}
