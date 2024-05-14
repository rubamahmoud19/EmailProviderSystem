﻿// <auto-generated />
using System;
using EmailProviderSystem.Data.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmailProviderSystem.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240513145318_AddInitailTables")]
    partial class AddInitailTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmailProviderSystem.Entities.Entities.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailId"));

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsImportant")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("EmailProviderSystem.Entities.Entities.Folder", b =>
                {
                    b.Property<int>("FolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FolderId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("User_Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FolderId");

                    b.HasIndex("UserEmail");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("EmailProviderSystem.Entities.Entities.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HashPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmailProviderSystem.Entities.Entities.Folder", b =>
                {
                    b.HasOne("EmailProviderSystem.Entities.Entities.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserEmail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmailProviderSystem.Entities.Entities.User", b =>
                {
                    b.Navigation("Folders");
                });
#pragma warning restore 612, 618
        }
    }
}