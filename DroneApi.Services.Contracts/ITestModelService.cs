using DroneApi.Core.Dtos.TestModel;
namespace DroneApi.Services.Contracts
{
    public interface ITestModelService
    {
        Task<IEnumerable<TestModelDto>> GetAllTestModelAsync(bool trackChanges);
        Task<TestModelDto> GetTestModelByIdAsync(Guid id, bool trackChanges);
        Task<TestModelDto> CreateTestModelAsync(TestModelForCreationDto testModel);
        Task DeleteTestModelAsync(Guid id, bool trackChanges);
    }
}