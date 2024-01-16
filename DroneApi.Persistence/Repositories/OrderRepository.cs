using DroneApi.Core.Contracts;
using DroneApi.Core.Entities;
using DroneApi.Persistence.Context;

namespace DroneApi.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<Order> GetOrderByIdAsync(Guid id, bool trackChanges) =>
            (await FindByConditionAsync(order => order.Id == id, trackChanges)).SingleOrDefault();
        public async Task<IEnumerable<Order>> GetAllOrderAsync(bool trackChanges) =>
             (await FindAllAsync(trackChanges)).OrderBy(x => x.Id).ToList();
        public async Task CreateOrderAsync(Order order) => await CreateAsync(order);
        public async Task UpdateOrderAsync(Order order) => await UpdateAsync(order);
        public async Task DeleteOrderAsync(Order order) => await DeleteAsync(order);
    }
}
