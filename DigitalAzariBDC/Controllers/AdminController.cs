using DigitalAzariBDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalAzari.Controllers
{
    public class AdminController : Controller
    {
        DigitalAzariEntities DB = new DigitalAzariEntities();
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Compaign()
        {
            return View();
        }
        public ActionResult Dealership()
        {
            return View();
        }
       

        public ActionResult CompaignInfo()
        {
            return View();
        }

        public ActionResult Appointments()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }
        public ActionResult UserInfo()
        {
            return View();
        }
    }
}