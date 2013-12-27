using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc4KnockoutCRUD.Models;
using PerpetuumSoft.Knockout;

namespace Mvc4KnockoutCRUD.Controllers
{
    public class SimpleListController : KnockoutController
    {
        //
        // GET: /SimpleList/

        public ActionResult Index()
        {
            var model = new SimpleListModel()
            {
                Items = new List<string> { "Nhật Nam", "Mỹ Hạnh", "Thảo Ly" }
            };
            return View(model);
        }

        public ActionResult AddItem(SimpleListModel model)
        {
            model.AddItem();
            return Json(model);
        }

    }
}
