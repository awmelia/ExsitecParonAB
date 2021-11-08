using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExsitecParonAB;
using ExsitecParonAB.Models;

namespace ExsitecParonAB.Controllers
{
    [HandleError]
    public class WarehousesController : Controller
    {
        private ParonABEntities1 db = new ParonABEntities1();
        ErrorLog log = new ErrorLog();

        public ActionResult Index()
        {
            try
            {
                return View(db.Warehouses.ToList());
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
        public ActionResult Create([Bind(Include = "warehouseNo,city")] Warehouse warehouse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cityExists = db.Warehouses.Any(x => x.city == warehouse.city);

                    if (cityExists)
                    {
                        ModelState.AddModelError("city", "En stad med detta namn existerar redan.");
                    }
                    db.Warehouses.Add(warehouse);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(warehouse);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
                ModelState.AddModelError("", "Staden kunde inte läggas till.");
            }
            return View();

        }

        // GET: Warehouses/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                 if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Warehouse warehouse = db.Warehouses.Find(id);
                if (warehouse == null)
                {
                    return HttpNotFound();
                }
                return View(warehouse);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "warehouseNo,city")] Warehouse warehouse)
        {
            try
            {
                var cityExists = db.Warehouses.Any(x => x.city == warehouse.city);

                if (ModelState.IsValid)
                {
                    if (cityExists)
                    {
                        ModelState.AddModelError("city", "En stad med detta namn existerar redan.");
                    }
                    db.Entry(warehouse).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(warehouse);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
                ModelState.AddModelError("", "Staden kunde inte ändras.");
            }
            return View();

        }

        // GET: Warehouses/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Warehouse warehouse = db.Warehouses.Find(id);
                if (warehouse == null)
                {
                    return HttpNotFound();
                }
                return View(warehouse);
            }
            catch (Exception ex)
            {
                log.ErrorLogging(ex);
            }
            return View();

            
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Warehouse warehouse = db.Warehouses.Find(id);
                db.Warehouses.Remove(warehouse);
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
