using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Models {
    public class User : BaseModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        [Required]
        public string Avatar { get; set; } = "path1.png";
    }
}
