using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalTerm.Models {
    public class Order : BaseModel {
        [Required]
        [Range(0, Int32.MaxValue)]
        public int TotalAmount { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int ReceivedAmount { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int CashBackAmount { get => ReceivedAmount - TotalAmount; set => _cashBackAmount = value; }
        // Backing Field
        private int _cashBackAmount;

        [Required]
        [JsonIgnore]
        public Customer Customer { get; set; }

        [Required]
        public List<OrderItem> OrderItems { get; set; }
    }
}
