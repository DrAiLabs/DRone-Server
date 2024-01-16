using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneApi.Core.Entities
{
    public class Order
    {
        [Column("OrderId")]
        public Guid Id { get; set; }
        //[Required(ErrorMessage = "The field Order Content is required")]
        //[MaxLength(300, ErrorMessage = "Maximum length for the Order Content is 300 characters.")]
        //public string? OrderContent { get; set; }
        [Required(ErrorMessage = "The field Business Location is required")]
        [MaxLength(300, ErrorMessage = "Maximum length for the Business Location is 300 characters.")]
        public string? BusinessLocation { get; set; }
        [Required(ErrorMessage = "The field Dropoff Location is required")]
        [MaxLength(300, ErrorMessage = "Maximum length for the DropOff Location is is 300 characters.")]
        public string? DropoffLocation { get; set; }
        //[Required]
        //public string? OrderStatus { get; set; }
        //[Required]
        //public string? OrderType { get; set;}

    }
}
