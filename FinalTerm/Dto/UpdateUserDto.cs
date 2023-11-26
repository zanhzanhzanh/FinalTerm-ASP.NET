using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinalTerm.Dto {
    public class UpdateUserDto {
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Avatar { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
