using System;
using System.Collections.Generic;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabel
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            builder.Property(x => x.LastUpdateDate)
                .IsRequired()//NOT NULL
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                //.HasDefaultValueSql("GETDATE()"); //SQL
                .HasDefaultValue(DateTime.Now.ToUniversalTime()); //DotNet


            //Index
            builder
                .HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade);



            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Tags) //Tem muitas Tabelas para muitos posts
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",//Nome da Tabela
                     post => post.HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTag_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag.HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));

        }
    }
}