using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Banners
    {
        public int id { get; set; }
        
        
        [Required(ErrorMessage = "This field is required")]
        [Display(Name ="Event OR Offer Name")]
        public string RelatedName  { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image field is required")]
        [Display(Name =("Banner Image"))]
        public string Ban_Img { get; set; }

    }
}