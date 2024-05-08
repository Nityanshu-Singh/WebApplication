using Assessment1.Models;
using Assessment1.Repository;
using Microsoft.AspNetCore.Mvc;
//using NuGet.Protocol.Core.Types;

namespace Assesment1.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _order;

        public OrderController(IOrderRepository repository)
        {
            _order = repository;
        }
        

        public IActionResult CustomerDetailsByOrderDate(DateTime orderDate)
        {
            var customers = _order.GetCustomersByOrderDate(orderDate);
            return View(customers);
        }

        public IActionResult DisplayBill(int orderId)
        {
            var bill = _order.GetBill(orderId);
            return Content(bill);
        }

        public IActionResult AllOrders()
        {
            var orders = _order.GetAll();
            return View(orders);
        }

        public IActionResult HighestOrderCustomer()
        {
            var highestOrderCustomer = _order.GetCustomerWithHighestOrder();
            return View(highestOrderCustomer);
        }

        
    }
}