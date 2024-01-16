using Microsoft.EntityFrameworkCore;
using DroneApi.Core.Contracts;
using DroneApi.Core.Entities;
using DroneApi.Persistence.Context;
namespace DroneApi.Persistence.Repositories
{
    public class TestModelRepository : RepositoryBase<TestModel>, ITestModelRepository
    {
        public TestModelRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

        public async Task CreateTestModelAsync(TestModel testModel) => await CreateAsync(testModel);

        public async Task DeleteTestModelAsync(TestModel testModel) => await DeleteAsync(testModel);

        public async Task<IEnumerable<TestModel>> GetAllTestModelAsync(bool trackChanges) =>
             await FindAllAsync(trackChanges).Result.OrderBy(x => x.Id).ToListAsync();

        public async Task<TestModel> GetTestModelByIdAsync(Guid id, bool trackChanges) =>
            await FindByConditionAsync(c => c.Id == id, trackChanges).Result.SingleOrDefaultAsync();
    }
}
