using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalTerm.Models {
    public class OrderItem : BaseModel {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int TotalAmount { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [JsonIgnore]
        public Order Order { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}
