using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DroneApi.Core.Dtos.ErrorModel;
using DroneApi.Core.Dtos.TestModel;
using DroneApi.Services.Contracts;

namespace DroneApi.Presentation.Controllers
{
    [Route("api/test-models")]
    [ApiController]
    public class TestModelController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TestModelController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTestModels()
        {
            var testModels = await _serviceManager.TestModelService.GetAllTestModelAsync(trackChanges: false);
            return Ok(testModels);
        }

        [HttpGet("{id:guid}", Name = "GetTestModelById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTestModelById(Guid id)
        {
            var testModel = await _serviceManager.TestModelService.GetTestModelByIdAsync(id,trackChanges: false);
            return Ok(testModel);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = (typeof(ErrorDetailsDto)))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = (typeof(ErrorDetailsDto)))]
        public async Task<IActionResult> DeleteTestModelById(Guid id)
        {
           await _serviceManager.TestModelService.DeleteTestModelAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTestModel([FromBody] TestModelForCreationDto testModel)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var createdTestModel = await _serviceManager.TestModelService.CreateTestModelAsync(testModel);
            return CreatedAtRoute("GetTestModelById", new { id = createdTestModel.Id }, createdTestModel);
        }


    }
}
