using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Models {
    public class Product : BaseModel {
        [Required]
        public string Barcode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int ImportPrice { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int RetailPrice { get; set; }
    }
}
