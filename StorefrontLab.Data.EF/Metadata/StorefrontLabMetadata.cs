using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //**I added this for validation and formatting for metadata (they're grey until we use them)

namespace StorefrontLab.Data.EF//.Metadata **this is so that the namespace matches with other partial classes
{
    //class StorefrontLabMetadata  **no tables with this name it just comes with the scaffold
    //{
    //}

    #region Products Metadata
    public class ProductsMetadata
    {
        //foreign key to manufacturers field and auto incremented there so just did the name for the space and the required **ask about this
        [Display(Name = "Manufacturer ID")]
        [Required(ErrorMessage = "*This is a required field")]
        public int ManufacturerID { get; set; }

        [Display(Name = "Inventory Status ID")] //**once again a foreign key probably messing this up
        [Required(ErrorMessage = "*This is a required field")]
        public int InventoryStatusID { get; set; }

        [Display(Name = "Category ID")] //**same another foreign key
        [Required(ErrorMessage = "*This is a required field")]
        public int CategoryID { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "*This is a required field")]
        [StringLength(50, ErrorMessage = "*Max length of this field is 50 characters.*")]
        public string ProductName { get; set; }


        //**didn't do name because i think it already looks ok

        [StringLength(20, ErrorMessage = "*Max length of this field is 20 characters.*")]
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string Color { get; set; }


        [UIHint("MultilineText")] //another UIHint so reason to be concerned here - it's max nvarchar in table
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string Description { get; set; }


        [Display(Name = "Serial Number")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [StringLength(50, ErrorMessage = "*Max length of this field is 50 characters.*")]
        public string SerialNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-N/A-]")] //**seems like it needs the placeholder defined and prob is maybe ask about this
        [Range(0, double.MaxValue, ErrorMessage = "*Value must be 0 or larger.*")]
        public Nullable<decimal> Price { get; set; }


        [Display(Name = "Product Image")] //did nothing for null format because we didn't in the book store**
        public string ProductImage { get; set; }
    }//end products metadata

    [MetadataType(typeof(ProductsMetadata))]
    public partial class Product { }

    #endregion


    #region Category Metadata


    public class CategoryMetadata
    {

        [Display(Name = "Category")]
        [StringLength(20, ErrorMessage = "*Max length of this field is 20 characters.*")]
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        [StringLength(100, ErrorMessage = "*Max length of this field is 100 characters.*")]
        [UIHint("MultilineText")] //***********************Is this right? If so where is the breaking point? It looks like it matches a textarea or something like that
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string Comments { get; set; }

    }//end category metadata
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    #endregion


    #region Customer Metadata

    //do this later when i know what this table does or does not do

    #endregion


    #region InventoryStatus Metadata
    public class InventoryStatusMetadata
    {
        //*********when would i add the primary key? Formatting the name? seems like it should happen here but i don't know

        //public int InventoryStatusID { get; set; }

        [Display(Name = "Inventory Status")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [StringLength(50, ErrorMessage = "*Max length of this field is 50 characters.*")]
        public string InventoryStatusName { get; set; }

        [StringLength(100, ErrorMessage = "*Max length of this field is 100 characters.*")]
        [UIHint("MultilineText")] //UI again google this already jon
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string Comments { get; set; }

    }

    [MetadataType(typeof(InventoryStatusMetadata))]
    public partial class InventoryStatus{}
    #endregion


    #region Manufacturer Metadata
    public class ManufactureMetadata
    {
        [Display(Name = "Manufacturer Name")]
        [Required(ErrorMessage = "*This is a required field")]
        [StringLength(50, ErrorMessage = "*Max length of this field is 50 characters.*")]
        public string ManufacturerName { get; set; }


        [StringLength(20, ErrorMessage = "*Max length of this field is 20 characters.*")]
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string City { get; set; }


        [StringLength(20, ErrorMessage = "*Max length of this field is 20 characters.*")]
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string State { get; set; }


        [StringLength(20, ErrorMessage = "*Max length of this field is 20 characters.*")]
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string Country { get; set; }


        [StringLength(20, ErrorMessage = "*Max length of this field is 20 characters.*")]
        [DisplayFormat(NullDisplayText = "- N/A -")]
        public string Warranty { get; set; }

        [Display(Name = "Logo")] //did nothing for null format because we didn't in the book store**
        public string Logo { get; set; }

    }

    [MetadataType(typeof(ManufactureMetadata))]
    public partial class Manufacturer { }
    #endregion
}//end namespace
