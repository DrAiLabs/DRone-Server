using System.ComponentModel.DataAnnotations;

namespace DroneApi.Core.Dtos.TestModel
{
    public record TestModelForCreationDto
    {
        [Required(ErrorMessage = "Test Model Name is a required field")]
        [MinLength(5, ErrorMessage = "Min length for the Name is 5 characters.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }
    };
}
