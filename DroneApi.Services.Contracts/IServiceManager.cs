namespace DroneApi.Services.Contracts
{
    public interface IServiceManager
    {
        public ITestModelService TestModelService { get; }
        public IOrderService OrderService { get; }
    }
}
