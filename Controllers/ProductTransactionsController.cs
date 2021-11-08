using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExsitecParonAB.Controllers;
using ExsitecParonAB.Models;

namespace ParonAB_WebApplication.Controllers
{
    [HandleError]
    public class ProductTransactionsController : Controller
    {
        private ParonABEntities1 db = new ParonABEntities1();
        DataAccessLayer dal = new DataAccessLayer();
        ErrorLog log = new ErrorLog();

        public ActionResult Index()
        {
            try
            {
                return View(db.ProductTransactions.ToList());
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.productName = new SelectList(db.Products, "productName", "productName");
                ViewBag.city = new SelectList(db.Warehouses, "city", "city");
                return View();
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dateTransaction,productName,city,amount")] ProductTransaction productTransaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ProductTransactions.Add(productTransaction);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.productName = new SelectList(db.Products, "productName", "productName", productTransaction.productName);
                ViewBag.city = new SelectList(db.Warehouses, "city", "city", productTransaction.city);
                return View(productTransaction);

            }catch(Exception ex){
                log.ErrorLogging(ex);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View();
            
        }

        public ActionResult Edit(string id1, string id2, int? id3)
        {
            try
            {
                if (id1 == null || id2 == null || id3 == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ProductTransaction productTransaction = db.ProductTransactions.Find(id1, id2, id3);

                if (productTransaction == null)
                {
                    return HttpNotFound();
                }
                ViewBag.dateTransaction = new SelectList(db.ProductTransactions, "dateTransaction", "dateTransaction", productTransaction.dateTransaction);
                ViewBag.productName = new SelectList(db.ProductTransactions, "productName", "productName", productTransaction.productName);
                ViewBag.city = new SelectList(db.ProductTransactions, "city", "city", productTransaction.city);
                ViewBag.amount = new SelectList(db.ProductTransactions, "amount", "amount", productTransaction.amount);
                return View(productTransaction);
            }catch(Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();
        }
            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dateTransaction,productName,city,amount")] ProductTransaction productTransaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(productTransaction).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.dateTransaction = new SelectList(db.ProductTransactions, "dateTransaction", "dateTransaction", productTransaction.dateTransaction);
                ViewBag.productName = new SelectList(db.ProductTransactions, "productName", "productName", productTransaction.productName);
                ViewBag.city = new SelectList(db.ProductTransactions, "city", "city", productTransaction.city);
                ViewBag.amount = new SelectList(db.ProductTransactions, "amount", "amount", productTransaction.amount);
                return View(productTransaction);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View();

        }

        public ActionResult Delete(string id1, string id2, int? id3)
        {
            try
            {
                if (id1 == null || id2 == null || id3 == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProductTransaction productTransaction = db.ProductTransactions.Find(id1, id2, id3);
                if (productTransaction == null)
                {
                    return HttpNotFound();
                }
                return View(productTransaction);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2, int? id3)
        {
            try
            {
                ProductTransaction productTransaction = db.ProductTransactions.Find(id1, id2, id3);
                db.ProductTransactions.Remove(productTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
