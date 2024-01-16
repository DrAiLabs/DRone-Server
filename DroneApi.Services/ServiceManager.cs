using AutoMapper;
using Microsoft.Extensions.Configuration;
using DroneApi.Core.Contracts;
using DroneApi.Services.Contracts;

namespace DroneApi.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITestModelService> _testModelService;
        private readonly Lazy<IOrderService> _orderService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IConfiguration config)
        {
            _testModelService = new Lazy<ITestModelService>(() => new TestModelService(repositoryManager, loggerManager, mapper));
            _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, loggerManager, mapper));
        }
        public ITestModelService TestModelService => _testModelService.Value;
        public IOrderService OrderService => _orderService.Value;
    }
}
