using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public string GetCalendarId()
        {
            GoogleCalendar calendar = new GoogleCalendar
           ("AIOP Groupe4", "aiop2013g4", "Adminaiop2013");
            return calendar.GetCalendarId();
        }

    }
}
