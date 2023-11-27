﻿using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class LoginDto {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
