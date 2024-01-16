using AutoMapper;
using DroneApi.Core.Dtos.OrderModel;
using DroneApi.Core.Entities;

namespace DroneApi.Core.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            //CreateMap<OrderCreateUpdateDto, Order>();
        }
    }
}
