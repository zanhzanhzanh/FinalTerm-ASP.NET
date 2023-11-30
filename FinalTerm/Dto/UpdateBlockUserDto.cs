using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class UpdateBlockUserDto {
        [Required]
        public bool IsBlocked{ get; set; }
    }
}
