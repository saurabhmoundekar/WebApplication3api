
using System.ComponentModel.DataAnnotations;

namespace WebApplication3API.Models
{
    public class Register
    {

        [Key]
        public int Id { get; set; }  // Primary Key

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress] // Ensures valid email format
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [Phone] // Ensures valid phone number
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        public string Password { get; set; } = string.Empty;


    }
}
