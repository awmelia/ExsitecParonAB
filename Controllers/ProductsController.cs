using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExsitecParonAB.Models;

namespace ExsitecParonAB.Controllers
{
    public class ProductsController : Controller
    {
        private ParonABEntities1 db = new ParonABEntities1();
        DataAccessLayer dal = new DataAccessLayer();
        ErrorLog log = new ErrorLog();

        public ActionResult Index()
        {
            try
            {
                return View(db.Products.ToList());
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "number,productNo,productName,price")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productNameExists = db.Products.Any(x => x.productName == product.productName);

                    if (productNameExists)
                    {
                        ModelState.AddModelError("productName", "En produkt med detta namn existerar redan.");
                    }

                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
                ModelState.AddModelError("", "Produkten kunde inte läggas till.");
            }
            return View();

        }


        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number,productNo,productName,price")] Product product)
        {
            try
            {
                var productExists = db.Products.Any(x => x.productName == product.productName);

                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
                ModelState.AddModelError("", "Produkten kunde inte ändras. Finns det redan en produkt med detta namn?");
            }
            return View();

        }

        public ActionResult Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<GetProducts_Result> GetProducts()
        {
            List<GetProducts_Result> list = new List<GetProducts_Result>();
            try
            {
                return dal.GetProducts();

            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return list;
        }

        public List<GetProductWarehouseAmount_Result> GetProductWarehouseAmount(string productName)
        {
            List<GetProductWarehouseAmount_Result> list = new List<GetProductWarehouseAmount_Result>();
            try
            {
                return dal.GetProductWarehouseAmount(productName);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return list;
        }
    }
}
