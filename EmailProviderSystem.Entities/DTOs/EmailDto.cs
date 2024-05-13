namespace EmailProviderSystem.Entities.DTOs
{
    public class EmailDto
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsImportant { get; set; } = false;

    }
}
