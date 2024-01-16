using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneApi.Core.Dtos.OrderModel
{
    public record OrderCreateUpdateDto
    {
        public Guid Id { get; set; }
        public string? BusinessLocation { get; set; }
        public string? DropoffLocation { get; set; }
    }
}
