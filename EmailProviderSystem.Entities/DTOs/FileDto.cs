namespace EmailProviderSystem.Entities.DTOs
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }

    }
}
