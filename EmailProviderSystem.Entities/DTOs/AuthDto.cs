using System.ComponentModel.DataAnnotations;


namespace EmailProviderSystem.Entities.DTOs
{
    public class AuthDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
