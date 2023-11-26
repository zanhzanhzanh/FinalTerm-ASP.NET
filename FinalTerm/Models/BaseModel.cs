using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Models {
    public class BaseModel {
        [Key]
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
