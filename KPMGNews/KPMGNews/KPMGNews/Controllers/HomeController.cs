using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KPMGNews.Models;

namespace KPMGNews.Controllers
{
    public class HomeController : Controller
    {
        //Home Controller for Home Page if available

        [SessionAuthorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Placeholder! See News PAGE!!!";
            return View();
        }

        [SessionAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [SessionAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}