using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc4KnockoutCRUD.Models;

namespace Mvc4KnockoutCRUD.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/

        public ActionResult Index()
        {
            return View(new HelloWorldModel
            {
                FirstName = "Võ",
                LastName = "Nhật Nam"
            });
        }

    }
}
