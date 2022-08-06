using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.ViewModels
{
    public class CardViewModel
    {
        public CardModel CardModel  { get; set; }
        public int Category_id { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}