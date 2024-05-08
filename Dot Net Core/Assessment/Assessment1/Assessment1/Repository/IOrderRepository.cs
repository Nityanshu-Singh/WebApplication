using Assessment1.Models;

namespace Assessment1.Repository
{
    public interface IOrderRepository
    {
        string GetBill(int orderId);
        List<Customer> GetCustomersByOrderDate(DateTime orderDate);

        List<Order> GetAll();

        Customer GetCustomerWithHighestOrder();

    }
}
