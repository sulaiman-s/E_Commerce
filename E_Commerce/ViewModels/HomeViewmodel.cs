using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.ViewModels
{
    public class HomeViewmodel
    {
        public List<CardModel> Cards { get; set; }
        public List<Banners> Banners { get; set; }
        public List<Category> Categories { get; set; }
    }
}