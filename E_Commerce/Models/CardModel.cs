using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Name field is required")]
        [Display(Name ="Name")]
        public string  ItemName { get; set; }

        [Required(ErrorMessage = "Image field is required")]
        [Display(Name ="Image")]
        public string ItemImage { get; set; }


        [Required(ErrorMessage = "Description field is required")]
        public string Description{ get; set; }


        [Required(ErrorMessage = "Price field is required")]
        [Display(Name = "Price")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Category field is required")]

        public Category Category { get; set; }

        [Required(ErrorMessage ="Product Code required")]
        public string  ProductCode { get; set; }

    }
}