using System.ComponentModel.DataAnnotations;

namespace EmailProviderSystem.Entities.Entities
{
    public class User
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string HashPassword { get; set; }
    }
}
