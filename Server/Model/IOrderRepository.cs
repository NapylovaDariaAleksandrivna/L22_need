namespace Server;

public interface IOrderRepository
{
    List<Order> GetAllOrders();
    void AddOrder(Order order);
    void RemoveOrder(string id);
}