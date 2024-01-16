using DroneApi.Core.Contracts;
using DroneApi.Persistence.Context;

namespace DroneApi.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ITestModelRepository> _testModelRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _testModelRepository = new Lazy<ITestModelRepository>(() => new TestModelRepository(repositoryContext));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(repositoryContext));

        }

        public ITestModelRepository TestModelRepository => _testModelRepository.Value;
        public IOrderRepository OrderRepository => _orderRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
