using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateOrderDto {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int ReceivedAmount { get; set; }

        //[Required]
        //public CreateCustomerDto Customer { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [BindRequired]
        public string Name { get; set; } = string.Empty;

        [BindRequired]
        public string Address { get; set; } = string.Empty;

        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
