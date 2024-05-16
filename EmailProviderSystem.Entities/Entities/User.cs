using System.ComponentModel.DataAnnotations;

namespace EmailProviderSystem.Entities.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public List<Folder> Folders { get; set; }
    }
}
