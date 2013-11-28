using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHocLapTrinhWeb.Models;

namespace MvcHocLapTrinhWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Tập làm quen với MVC";
            HLTWDB h = new HLTWDB();
            var query = from n in h.tbl_Newss
                        orderby  Guid.NewGuid()
                        select n;
            return View(query.Skip(0).Take(10).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
