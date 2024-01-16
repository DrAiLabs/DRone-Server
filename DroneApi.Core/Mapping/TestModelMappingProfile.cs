using AutoMapper;
using DroneApi.Core.Dtos.TestModel;
using DroneApi.Core.Entities;
namespace DroneApi.Core.Mappings
{
    public class TestModelMappingProfile : Profile
    {
        public TestModelMappingProfile() 
        {
            CreateMap<TestModel, TestModelDto>();
            CreateMap<TestModelForCreationDto, TestModel>();
        }
    }
}
