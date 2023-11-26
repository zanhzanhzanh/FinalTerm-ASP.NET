using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateOrderDto {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int ReceivedAmount { get; set; }

        [Required]
        public CreateCustomerDto Customer { get; set; }

        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
