namespace DroneApi.Core.Dtos.OrderModel
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public string? BusinessLocation { get; set; }
        public string? DropoffLocation { get; set; }
    }
}
