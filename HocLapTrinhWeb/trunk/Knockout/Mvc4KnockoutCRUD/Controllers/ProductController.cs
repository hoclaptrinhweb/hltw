using Mvc4KnockoutCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc4KnockoutCRUD.Controllers
{
    public class ProductController : Controller
    {
        static readonly IProductRepository repository = new ProductRepository();

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult TestProduct()
        {
            var p = new Product()
            {
                Name = "Nhatnam",
                Id = 1,
                Price = 1,
                Category = "loại"
            };
            return View(p);
        }

        public JsonResult GetAllProducts()
        {
            return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(Product item)
        {
            item = repository.Add(item);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProduct(int id, Product product)
        {
            product.Id = id;
            if (repository.Update(product))
            {
                return Json(repository.GetAll(), JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        public JsonResult DeleteProduct(int id)
        {

            if (repository.Delete(id))
            {
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = false }, JsonRequestBehavior.AllowGet);


        }
    }
}
