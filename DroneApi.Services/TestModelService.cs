using AutoMapper;
using DroneApi.Core.Contracts;
using DroneApi.Core.Dtos.TestModel;
using DroneApi.Core.Entities;
using DroneApi.Core.Exceptions;
using DroneApi.Services.Contracts;
namespace DroneApi.Services
{
    internal sealed class TestModelService : ITestModelService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public TestModelService(IRepositoryManager repositoryManager,ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<TestModelDto> CreateTestModelAsync(TestModelForCreationDto testModel)
        {
            if (testModel is null) throw new BadRequestException("Test model object is null");

            var testModelEntity = _mapper.Map<TestModel>(testModel);
            await _repositoryManager.TestModelRepository.CreateTestModelAsync(testModelEntity);
            await _repositoryManager.SaveAsync();

            var testModelToReturn = _mapper.Map<TestModelDto>(testModelEntity);
            return testModelToReturn;
        }

        public async Task DeleteTestModelAsync(Guid id, bool trackChanges)
        {
            var testModel = await _repositoryManager.TestModelRepository.GetTestModelByIdAsync(id, trackChanges);
            if (testModel is null) throw new NotFoundException($"Test Model with id: {id} does not exist in the database");

            await _repositoryManager.TestModelRepository.DeleteTestModelAsync(testModel);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<TestModelDto>> GetAllTestModelAsync(bool trackChanges)
        {
            _loggerManager.LogInfo("GetAllTestModelAsync");

            var testModels = await _repositoryManager.TestModelRepository.GetAllTestModelAsync(trackChanges);
            var testModelsDto = _mapper.Map<IEnumerable<TestModelDto>>(testModels);

            return testModelsDto;
        }

        public async Task<TestModelDto> GetTestModelByIdAsync(Guid id, bool trackChanges)
        {
            var testModel = await _repositoryManager.TestModelRepository.GetTestModelByIdAsync(id, trackChanges);
            if (testModel is null) throw new NotFoundException($"The test model with id : {id} does not exist in the database.");

            var testModelDto = _mapper.Map<TestModelDto>(testModel);
            return testModelDto;
        }
    }
}
