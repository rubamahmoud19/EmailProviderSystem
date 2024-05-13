using EmailProviderSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailProviderSystem.Data.Database.Configs
{
    internal class FolderConfig : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> modelBuilder)
        {
            modelBuilder.Property(u => u.Name).IsRequired();
            modelBuilder.HasOne(u => u.User).WithMany(u => u.Folders)
                .HasForeignKey(u => u.User_Email)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
