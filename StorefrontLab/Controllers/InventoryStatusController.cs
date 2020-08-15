using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StorefrontLab.Data.EF;

namespace StorefrontLab.Controllers
{
    public class InventoryStatusController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: InventoryStatus
        public ActionResult Index()
        {
            return View(db.InventoryStatuses.ToList());
        }

        // GET: InventoryStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryStatus inventoryStatus = db.InventoryStatuses.Find(id);
            if (inventoryStatus == null)
            {
                return HttpNotFound();
            }
            return View(inventoryStatus);
        }

        // GET: InventoryStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryStatusID,InventoryStatusName,Comments")] InventoryStatus inventoryStatus)
        {
            if (ModelState.IsValid)
            {
                db.InventoryStatuses.Add(inventoryStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventoryStatus);
        }

        // GET: InventoryStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryStatus inventoryStatus = db.InventoryStatuses.Find(id);
            if (inventoryStatus == null)
            {
                return HttpNotFound();
            }
            return View(inventoryStatus);
        }

        // POST: InventoryStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryStatusID,InventoryStatusName,Comments")] InventoryStatus inventoryStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventoryStatus);
        }

        // GET: InventoryStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryStatus inventoryStatus = db.InventoryStatuses.Find(id);
            if (inventoryStatus == null)
            {
                return HttpNotFound();
            }
            return View(inventoryStatus);
        }

        // POST: InventoryStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryStatus inventoryStatus = db.InventoryStatuses.Find(id);
            db.InventoryStatuses.Remove(inventoryStatus);
            db.SaveChanges();
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
    }
}
