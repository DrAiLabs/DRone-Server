using DroneApi.Core.Entities;

namespace DroneApi.Core.Contracts
{
    public interface ITestModelRepository
    {
        Task<IEnumerable<TestModel>> GetAllTestModelAsync(bool trackChanges);
        Task<TestModel> GetTestModelByIdAsync(Guid id, bool trackChanges);
        Task CreateTestModelAsync(TestModel testModel);
        Task DeleteTestModelAsync(TestModel testModel);
    }
}
