namespace EmailProviderSystem.Entities.Entities
{
    public class Folder
    {
        public int FolderId {  get; set; }
        public string Name { get; set; }
        public string User_Email { get; set; }
        public User User { get; set; }

        public List<Email> Emails { get; set; }
    }
}
