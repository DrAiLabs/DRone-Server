using AutoMapper;
using DroneApi.Core.Contracts;
using DroneApi.Core.Entities;
using DroneApi.Core.Exceptions;
using DroneApi.Services.Contracts;
using DroneApi.Core.Dtos.OrderModel;

namespace DroneApi.Services
{
    internal sealed class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync(bool trackChanges)
        {
            _loggerManager.LogInfo($"OrderService::GetAllOrderAsync(TrackChanges: {trackChanges})");

            var orders = await _repositoryManager.OrderRepository.GetAllOrderAsync(trackChanges);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id, bool trackChanges)
        {
            var order = await _repositoryManager.OrderRepository.GetOrderByIdAsync(id, trackChanges);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto order)
        {
            if (order == null)  throw new ArgumentNullException($"The field: {nameof(order)} cannot be null");

            var orderEntity = _mapper.Map<Order>(order);
            await _repositoryManager.OrderRepository.CreateOrderAsync(orderEntity);
            await _repositoryManager.SaveAsync();

            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return orderToReturn;
        }

        public async Task UpdateOrderAsync(Guid id, OrderDto updatedOrder)
        {
            var originalOrder = await _repositoryManager.OrderRepository.GetOrderByIdAsync(id, true);
            if (originalOrder == null) throw new NotFoundException($"The order with id: {id} does not exist.");

            _mapper.Map(updatedOrder, originalOrder);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteOrderAsync(Guid id, bool trackChanges)
        {
            var order = await _repositoryManager.OrderRepository.GetOrderByIdAsync(id, trackChanges);
            if (order == null) throw new NotFoundException($"The order with id: {id} does not exist.");

            await _repositoryManager.OrderRepository.DeleteOrderAsync(order);
            await _repositoryManager.SaveAsync();
        }
    }
}
