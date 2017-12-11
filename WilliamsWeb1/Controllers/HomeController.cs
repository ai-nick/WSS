using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WilliamsWeb1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Please read the following to get a brief overview of how to use the site to start a project with me!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you don't want to use the sites messenger you can also contact me via the following channels.";

            return View();
        }
    }
}