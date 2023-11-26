using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class CreateUserDto {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Fullname { get; set; }

        [BindRequired]
        public string Username { get; set; } = string.Empty;
    }
}
