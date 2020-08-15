using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StorefrontLab.Data.EF;
using PagedList;
using PagedList.Mvc;

using StorefrontLab.Models;

namespace StorefrontLab.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        #region AJAX Delete
        [HttpPost]
        public JsonResult AjaxDelete(int id)
        {
            //retrieve the product from the data base
            Product productToBeRemoved = db.Products.Find(id);

            //remove said product
            db.Products.Remove(productToBeRemoved);

            //save the changes
            db.SaveChanges();

            //Create message to send back to UI as a JSON result also what is json
            var message = $"Deleted Product {productToBeRemoved.ProductName} from the database!";
            return Json(
                new
                {
                    id = id,
                    message = message
                });
        }
        #endregion

        //#region AJAX Create
        ////Add Publisher to the database via AJAX and return results
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult ProductCreate(Product product)
        //{
        //    db.Products.Add(product);
        //    db.SaveChanges();

        //    return Json(product);
        //}

        //#endregion

        #region Ajax edit get and post

        [HttpGet]
        public PartialViewResult ProductEdit(int id)
        {
            Product pro = db.Products.Find(id);
            return PartialView(pro);
        }
        
        [HttpPost]
        public JsonResult ProductEdit(Product pro)
        {
            db.Entry(pro).State = EntityState.Modified;
            db.SaveChanges();
            return Json(pro);

        }




        #endregion

        //#region AJAX Details
        //[HttpGet]
        //public PartialViewResult ProductDetails(int id)
        //{
        //    //Retrieve the product by it's id
        //    Product pro = db.Products.Find(id);
        //    //Return a partial lview to the browser with the publisher object
        //    return PartialView(pro);

        //    //Set up this view
        //    //1)right click and add a partial view
        //    //2) scaffold details
        //    //3)select partial view

        //}





//#endregion
        //// GET: Products
        //public ActionResult Index()
        //{
        //    var products = db.Products.Include(p => p.Category).Include(p => p.InventoryStatus).Include(p => p.Manufacturer);
        //    return View(db.Products.ToList());
        //}

        // GET: Products/Details/5
        public ActionResult Details(int? id)
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.InventoryStatusID = new SelectList(db.InventoryStatuses, "InventoryStatusID", "InventoryStatusName");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ManufacturerID,InventoryStatusID,CategoryID,ProductName,Color,Description,SerialNumber,Price,ProductImage")]
        Product product, HttpPostedFileBase ImageUpload)
        {
            
            if (ModelState.IsValid)
            {
                //only process the file if all the other validation has passed (modelState.IsValid_
                #region File Upload
                //provide a default img incase the user doesn't provide one
                string imageName = "noImage.jpg";

                //check the input and process whether its value is !null
                if (ImageUpload != null)
                {
                    //get the file name and save it to variable
                    imageName = ImageUpload.FileName;

                    //use the file name and extract the extension and save it ot a variable (important for images and control of sizes)
                    string ext = imageName.Substring(imageName.LastIndexOf("."));//returns .png or .jpg

                    //create a list of expressions we will take
                    string[] goodExts = new string[] { ".jpeg", ".jpg", ".png", ".gif" };

                    //compare the extension to the list
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //here we rename the file (conventionally use a guid) could do other things but just try to make one work then worry about it
                        imageName = Guid.NewGuid() + ext;

                        //now save the image to the web server
                        ImageUpload.SaveAs(Server.MapPath("~/Content/images/" + imageName));
                    }
                    //if they give us an invalid extension we want to just display the default image
                    else
                    {
                        imageName = "noImage.jpg";
                    }//end if imageUpload !=null
                    //no matter which way it went, need to update the db with the correct imagename for the record
                    product.ProductImage = imageName;
                    #endregion
                    //sending the info to entity framework
                    db.Products.Add(product);
                    //save the changes to the db
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }



                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.InventoryStatusID = new SelectList(db.InventoryStatuses, "InventoryStatusID", "InventoryStatusName", product.InventoryStatusID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.InventoryStatusID = new SelectList(db.InventoryStatuses, "InventoryStatusID", "InventoryStatusName", product.InventoryStatusID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ManufacturerID,InventoryStatusID,CategoryID,ProductName,Color,Description,SerialNumber,Price,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.InventoryStatusID = new SelectList(db.InventoryStatuses, "InventoryStatusID", "InventoryStatusName", product.InventoryStatusID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "ManufacturerName", product.ManufacturerID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //remove the original file from the edit view
           
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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



        //Server side filter action
        //public ActionResult ProductsQs(string productsFilter)
        //{
        //    if (String.IsNullOrEmpty(productsFilter))
        //    {
        //        //this is here so that if they don't search, return all records
        //        return View(db.Products.ToList());
        //    }
        //    else
        //    {
        //        //if they search, compare it to parameters and use linq method syntax       ****why product not products for list? figure out where that comes 
        //        string searchUpCase = productsFilter.ToUpper();
        //        List<Product> searchResults = db.Products
        //                    .Where(p => p.ProductName.ToUpper().Contains(searchUpCase))
        //                    .OrderBy(p => p.ProductName)
        //                    .ToList();

        //        return View(searchResults);
        //    }
        //}//end of searchFilter
        //
        public ViewResult Index(string searchString, string currentFilter, int page = 1) //had to rename this for some reason Jared helped me this is PAGING*****
        {
            int pageSize = 5;
            var products = db.Products.OrderBy(p => p.ProductName).ToList();

            if (searchString != null)
            {
                page = 1;
            }else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                products = (from p in products
                            where p.ProductName.ToUpper().Contains(searchString.ToUpper())
                            orderby p.ProductName
                            select p).ToList();
            }
            ViewBag.CurrentFilter = searchString;
            return View(products.ToPagedList(page,pageSize));
        }

        [HttpPost]
        public ActionResult AddToCart(int qty, int productId)
        {
            //Create a shell "Local" shopping cart
            Dictionary<int, ShoppingCartViewModel> shoppingCart = null;

            //Check the global shopping cart
            if (Session["cart"] != null)
            {
                //if it has stuff in it, reassign to the local
                shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];
            }
            else
            {
                //create an empty local version 
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
            }

            //get the product being displayed in the view
            Product product = db.Products.Where(x => x.ProductID == productId).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //title is valid
                ShoppingCartViewModel item = new ShoppingCartViewModel(qty, product);
                //make sure the item isn't already in the cart and if it is, just increase qty
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    //add the item to the cart if it wasn't alredy there
                    shoppingCart.Add(product.ProductID, item);
                }
                //now that the item has been added to the local cart, update the session cart with new item/qty
                Session["cart"] = shoppingCart;
                Session["confirm"] = string.Format($"{((qty > 1) ? "were" : "was")} was added to your cart.");
            }
            return RedirectToAction("index", "ShoppingCart");
        }
    }//end class
}//end namespace
