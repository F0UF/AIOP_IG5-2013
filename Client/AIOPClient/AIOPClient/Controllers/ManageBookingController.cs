using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIOPClient.Models;

namespace AIOPClient.Controllers
{
    public class ManageBookingController : Controller
    {
        //
        // GET: /ManageBooking/

        public ActionResult Index()
        {
            UserSession session = null;
            session = UserSession.GetInstance();
            if (!session.logged)
            {
                return Redirect("../Home/Index");
            }
            else
                return View();
        }

    }
}
