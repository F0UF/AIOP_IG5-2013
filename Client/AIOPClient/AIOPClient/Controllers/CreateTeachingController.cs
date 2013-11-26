using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AIOPClient.Controllers
{
    public class CreateTeachingController : Controller
    {
        //
        // GET: /CreateTeaching/
        [HttpGet]
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

                    result = client.DownloadString("http://aiopninjaserver.no-ip.biz/api/teacher/accountsList");
                    jsonVal = JArray.Parse(result) as JArray;
                    dynamic teachers = jsonVal;
                    ViewBag.TeacherList = teachers;

                    return View();
                }
            }
        }

        // POST: /CreateTeaching/

        [HttpPost]
        public ActionResult Index(String subject, String teacher, String group, String type, String course_name, String hours)
        {
            using (var client = new WebClient())
            {
                UserSession session = null;
                session = UserSession.GetInstance();
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                JObject json = new JObject();
                json.Add("Subject_Name", subject);
                json.Add("Teacher_Name", teacher);
                json.Add("Group_Name", group);
                json.Add("Type", type);
                json.Add("Course_Name", course_name);
                json.Add("Hours", int.Parse(hours));

                String urlApi = "http://aiopninjaserver.no-ip.biz/api/admin/CreateTeaching";
                string jsonString = json.ToString();
                var response = client.UploadString(urlApi, "POST", json.ToString());

                if (response.Equals("null"))
                {
                    return Content("<script> alert(\'Error : teaching not created\');location.href='./';</script>");
                }
                else
                {
                    return Content("<script> alert(\'Teaching created\');location.href='./';</script>");
                }
            }
        }

    }
}
