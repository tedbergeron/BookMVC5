using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator calc;

        private Product[] products = {
                                         new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                                         new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                                         new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                                         new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                                     };

        public HomeController(IValueCalculator calcParam)
        {
            calc = calcParam;
        }


        //
        // GET: /Home/
        public ActionResult Index()
        {
            // 1) e.g. tightly coupled
            //LinqValueCalculator calc = new LinqValueCalculator();     // replacing with interface to decouple
            // 2) e.g. decoupling in C#
            //IValueCalculator calc = new LinqValueCalculator();        // replacing this with Ninject
            // 3) e.g. using Ninject 
            //IKernel ninjectKernal = new StandardKernel();
            //ninjectKernal.Bind <IValueCalculator>().To<LinqValueCalculator>();
            //IValueCalculator calc = ninjectKernal.Get<IValueCalculator>();
            // -------------------------------------------------
            // 4) finally use IValueCalulator (above) with public HomeController(IValueCalculator...) 
            // to decouple completely HomeController from LinqValueCalculator class
            // 


            ShoppingCart cart = new ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();
            
            return View(totalValue);
        }
	}
}