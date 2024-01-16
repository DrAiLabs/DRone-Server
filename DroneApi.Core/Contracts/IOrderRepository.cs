using DroneApi.Core.Entities;

namespace DroneApi.Core.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrderAsync(bool trackChanges);
        Task<Order> GetOrderByIdAsync(Guid id, bool trackChanges);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
    }
}
