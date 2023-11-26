using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateOrderItemDto {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}
