using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateCustomerDto {
        [Required]
        [Phone]
        public string Phone { get; set; }

        [BindRequired]
        public string Name { get; set; } = string.Empty;

        [BindRequired]
        public string Address { get; set; } = string.Empty;
    }
}
