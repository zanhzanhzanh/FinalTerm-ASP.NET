using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Models {
    public class Customer : BaseModel {
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
