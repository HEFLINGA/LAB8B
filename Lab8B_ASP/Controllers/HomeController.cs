using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab8B_ASP.Models;

namespace Lab8B_ASP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Kayak";
            string productName = myProduct.Name;
            return View("Result", (object)String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one peron",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "Apple", "Orange", "Plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"Apple", 10}, {"Orange", 20}, {"Plum", 30}
            };
            return View("Result", (object)stringArray[1]);
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "LifeJacket", Price = 48.95M},
                    new Product {Name = "Soccer Ball", Price = 19.50M},
                    new Product {Name = "Corner Flag", Price = 34.95M}
                }
            };
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "LifeJacket", Price = 48.95M},
                new Product {Name = "Soccer Ball", Price = 19.50M},
                new Product {Name = "Corner Flag", Price = 34.95M}
            };
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = products.TotalPrices();
            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseExtensions()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "LifeJacket", Price = 48.95M},
                    new Product {Name = "Soccer Ball", Price = 19.50M},
                    new Product {Name = "Corner Flag", Price = 34.95M}
                }
            };
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                    new Product {Name = "LifeJacket", Category = "Watersports", Price = 48.95M},
                    new Product {Name = "Soccer Ball", Category = "Soccer", Price = 19.50M},
                    new Product {Name = "Corner Flag", Category = "Soccer", Price = 34.95M}
                }
            };
            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";
            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "Soccer" || prod.Price > 20))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new { Name = "MVC", Category = "Patern" },
                new { Name = "Hat", Category = "Clothing" },
                new { Name = "Apple", Category = "Fruit" }
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product { Name = "Kayak", Category = "WaterSports", Price = 275M},
                new Product { Name = "LifeJacket", Category = "WaterSports", Price = 48.95M},
                new Product { Name = "Soccer Ball", Category = "Soccer", Price = 19.50M},
                new Product { Name = "Corner Flag", Category = "Soccer", Price = 34.95M},
            };
            /*
            var foundProducts = from match in products
                                orderby match.Price descending
                                select new { match.Name, match.Price }; */
            var foundProducts = products.OrderByDescending(e => e.Price).Take(3).Select(e => new { e.Name, e.Price });
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach(var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }
    }
}