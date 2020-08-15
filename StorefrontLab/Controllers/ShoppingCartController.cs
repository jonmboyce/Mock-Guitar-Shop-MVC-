using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontLab.Models; //what is happening here, this just links to the viewmodel? 

namespace StorefrontLab.Controllers
{
    public class ShoppingCartController : Controller
    {
        //Get Shopping Cart
        public ActionResult Index()
        {
            //create a local version of the shopping cart from teh Session(global)version
            var shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            // if the value is  null or the count is 0, create an empty instance and provide no cart items.
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //new empty instance of the local shopping cart to pass to the view
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
                ViewBag.Message = ("Your cart is empty");
            }
            else //if there is something in the cart
            {
                ViewBag.Message = null; //i think this means that we do not send a message at all **
            }
            return View(shoppingCart);
        }//end of shopping cart index action

        public ActionResult UpdateCart(int productId, int qty)
        {
            //get the cart from session and hold it in a dictionary 
            Dictionary<int, ShoppingCartViewModel> shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //update the cart locally - for the item that is selected (row that the update button was clicked in)

            if (qty > 0)
            {
                shoppingCart[productId].Qty = qty;
                //return the cart back to session for use
                Session["cart"] = shoppingCart;
            }
            else
            {
                ViewBag.Message("No items exist in your cart");
            }
            return RedirectToAction("Index");
        }//end of update cart action

        public ActionResult RemoveFromCart(int id)
        {
            //get the cart out of session
            Dictionary<int, ShoppingCartViewModel> shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //remove the items
            shoppingCart.Remove(id);
            //set up viewbag message for no results
            if(shoppingCart.Count == 0)
            {
                ViewBag.Message = ("There are no items in your cart.");

                //set the current global session to null
                Session["cart"] = null;
            }

            //reload the index
            return RedirectToAction("Index");
        }
    }
}