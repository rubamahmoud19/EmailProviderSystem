namespace EmailProviderSystem.Entities.Entities
{
    public class Email
    {
        public int EmailId { get; set; }
        public Guid MessageId { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public List<string> To { get; set; }

        public List <string> Cc { get; set; }

        public bool IsRead { get; set; } = false;
        public bool IsImportant { get; set; } = false;

        public int Folder_Id {  get; set; }

        public Folder Folder { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
