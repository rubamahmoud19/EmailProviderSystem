namespace EmailProviderSystem.Entities.DTOs
{
    public class FolderFilesDto
    {
        public string Name { get; set; }
        public List<string> FileNames { get; set; }
        public List<string> SubFolderNames { get; set; }
    }
}
