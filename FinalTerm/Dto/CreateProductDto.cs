using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateProductDto {
        [Required]
        [RegularExpression("^\\d+$")]
        public string Barcode { get; set; }

        [Required]
        public string Name { get; set; }

        [BindRequired]
        public string Category { get; set; } = string.Empty;

        [Required]
        [Range(0, Int32.MaxValue)]
        public int ImportPrice { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int RetailPrice { get; set; }
    }
}
