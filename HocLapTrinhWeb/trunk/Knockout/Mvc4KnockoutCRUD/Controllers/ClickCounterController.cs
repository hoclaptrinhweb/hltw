using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc4KnockoutCRUD.Models;

namespace Mvc4KnockoutCRUD.Controllers
{
    public class ClickCounterController : Controller
    {
        //
        // GET: /ClickCounter/

        public ActionResult Index()
        {
            return View(new ClickCounterModel());
        }

        public ActionResult RegisterClick(ClickCounterModel model)
        {
            model.RegisterClick();
            return Json(model);
        }

        public ActionResult ResetClicks(ClickCounterModel model)
        {
            model.RegisterClick();
            return Json(model);
        }

    }
}
