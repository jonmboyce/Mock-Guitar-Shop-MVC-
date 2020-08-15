using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StorefrontLab.Data.EF;

namespace StorefrontLab.Models
{
    public class ShoppingCartViewModel
    {
        [Range(1, int.MaxValue)]//this ensures that the value is greater than zero and up to the max value of the datatype
        public int Qty { get; set; }
        public Product Product { get; set; }

        //This is a fully qualified constructor to make it easy to store all the info **needs to be the same as the class
        public ShoppingCartViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }
    }
}