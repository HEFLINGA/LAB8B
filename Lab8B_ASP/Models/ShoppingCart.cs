using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace Lab8B_ASP.Models
{
    public class ShoppingCart : IEnumerable<Product>
    {
        public List<Product> Products { get; set; }
        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}