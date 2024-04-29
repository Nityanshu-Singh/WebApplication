using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question1.Models;

namespace Question1.Controllers
{
    public class CodeController : Controller
    {
        private NorthWindEntities1 db = new NorthWindEntities1(); // Replace NorthwindEntities with your generated DbContext

        // Action method to return customers residing in Germany
        public ActionResult GermanCustomers()
        {
            var germanCustomers = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(germanCustomers);
        }

        // Action method to return customer details with orderId == 10248
        public ActionResult CustomerDetailsWithOrder()
        {
            var customerWithOrder = db.Customers
                .Where(c => c.Orders.Any(o => o.OrderID == 10248))
                .FirstOrDefault();
            return View(customerWithOrder);
        }
    }
}