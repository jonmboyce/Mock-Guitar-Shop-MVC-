using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StorefrontLab.Data.EF;
using PagedList.Mvc;
using PagedList;

namespace StorefrontLab.Controllers
{
    public class ManufacturersController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Manufacturers
        public ViewResult Index(int page =1)
        {
            int pageSize = 3;

            var manufacturers = db.Manufacturers.OrderBy(x => x.ManufacturerName).ToList();
            return View(manufacturers.ToPagedList(page, pageSize));
        }

        // GET: Manufacturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManufacturerID,ManufacturerName,Logo,City,State,Country,Warranty")] Manufacturer manufacturer,
            HttpPostedFileBase logo)
        {
            #region FileUpload
            if (ModelState.IsValid)
            {
                string imgName = "noImage.jpg";
                if (logo != null)
                {
                    //get image and assign it to the variable
                    imgName = logo.FileName;

                    //declare and assign ext value from the substring where it will grab after the .
                    string ext = imgName.Substring(imgName.LastIndexOf("."));

                    //make a list of image extensions we will accept here
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower())&& (logo.ContentLength <= 4194304))//this is .net 4mb amx
                    {
                        //if it is ok and the extension is in the list we want to rename it with a guid (global unique id)
                        imgName = Guid.NewGuid() + ext;

                        //save it to the webserver
                        logo.SaveAs(Server.MapPath("~Content/images/" + "imgName"));
                    } else
                    {
                        imgName = "noImage.jpg";
                    }
                }
                //no matter which way that ended up we need to add the name to the object
                manufacturer.Logo = imgName;
                #endregion
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManufacturerID,ManufacturerName,Logo,City,State,Country,Warranty")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturer);
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
