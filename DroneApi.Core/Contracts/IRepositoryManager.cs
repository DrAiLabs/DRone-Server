namespace DroneApi.Core.Contracts
{
    public interface IRepositoryManager
    {
        ITestModelRepository TestModelRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task SaveAsync();
    }
}
