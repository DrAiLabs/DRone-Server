namespace DroneApi.Core.Dtos.TestModel
{
    public record TestModelDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
    }
}
