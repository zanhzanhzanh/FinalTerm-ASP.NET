using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class UpdateProductDto {
        [RegularExpression("^\\d+$")]
        public string? Barcode { get; set; }

        public string? Name { get; set; }

        public string? Category { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? ImportPrice { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? RetailPrice { get; set; }
    }
}
