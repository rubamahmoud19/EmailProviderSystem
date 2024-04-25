using System.ComponentModel.DataAnnotations;


namespace EmailProviderSystem.Entities.DTOs
{
    public class TokenDto
    {
        [Required]
        [EmailAddress]
        public string Token { get; set; }
    }

}
