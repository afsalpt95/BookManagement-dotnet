
using System.ComponentModel.DataAnnotations;

namespace BookManagement.DTOs.UserDTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string Interests { get; set; }
    }
}
