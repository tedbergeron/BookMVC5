using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper discounter;
        private static int counter = 0;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;

            // debug will print to VS-2012 Output window
            System.Diagnostics.Debug.WriteLine(string.Format("Instance {0} created", ++counter));
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            //return products.Sum(p => p.Price);    // replacing this with:
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}