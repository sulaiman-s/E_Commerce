using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.ViewModels
{
    public class CartViewModel
    {
        public List<CardModel>  CartList { get; set; }
    }
}