using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

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

    }
}
