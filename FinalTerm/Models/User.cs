using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public string Avatar { get; set; } = "path1.png";

        [Required]
        public bool IsBlocked { get; set; } = false;
    }
}
