using System.Text.Json;

namespace DroneApi.Core.Dtos.ErrorModel
{
    public class ErrorDetailsDto
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
