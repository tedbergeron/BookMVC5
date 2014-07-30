using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        //private LinqValueCalculator calc;
        private IValueCalculator calc;  // replacing above with this interface

        //public ShoppingCart(LinqValueCalculator calcParam)    // replacing with interface:
        public ShoppingCart(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(Products);
        }
    }
}