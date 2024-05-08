using Microsoft.EntityFrameworkCore;
using Assessment1.Models;


namespace Assessment1.Repository
{
    public class OrderService : IOrderRepository
    {
        private readonly NorthWindContext _context;
        public OrderService(NorthWindContext context)
        {

            _context = context;
        }
        public string GetBill(int ordId)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == ordId);

            if (order == null)
            {
                return "Order not found.";
            }

            decimal totalBill = 0;
            foreach (var orderDetail in order.OrderDetails)
            {
                totalBill += orderDetail.Quantity * orderDetail.UnitPrice;
            }

            return $"Total bill for Order ID {ordId}: ${totalBill}";
        }


        public List<Customer> GetCustomersByOrderDate(DateTime orderDate)
        {
            return _context.Customers.Where(c => c.Orders.Any(o => o.OrderDate == orderDate)).ToList();
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Customer GetCustomerWithHighestOrder()
        {
            return _context.Orders
                .GroupBy(x => x.CustomerId)
                .OrderByDescending(z => z.Count())
                .Select(y => y.First().Customer)
                .FirstOrDefault();
        }

       
    }
}
