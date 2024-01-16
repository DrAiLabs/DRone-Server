using DroneApi.Core.Dtos.OrderModel;

namespace DroneApi.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrderAsync(bool trackChanges);
        Task<OrderDto> GetOrderByIdAsync(Guid id, bool trackChanges);
        Task<OrderDto> CreateOrderAsync(OrderDto order);
        Task UpdateOrderAsync(Guid id, OrderDto order);
        Task DeleteOrderAsync(Guid id, bool trackChanges);
    }
}
