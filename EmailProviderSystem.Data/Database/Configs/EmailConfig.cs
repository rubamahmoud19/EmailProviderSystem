using EmailProviderSystem.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Data.Database.Configs
{
    internal class EmailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> modelBuilder)
        {
            modelBuilder.Property(u => u.From).IsRequired();

            modelBuilder.HasOne(u => u.Folder).WithMany(u => u.Emails)
                .HasForeignKey(u => u.FolderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
