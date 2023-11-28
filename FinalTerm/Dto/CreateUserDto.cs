using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateUserDto {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Fullname { get; set; }
    }
}
