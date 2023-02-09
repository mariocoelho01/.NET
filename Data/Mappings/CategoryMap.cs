using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Tabel
            builder.ToTable("Category");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(); //Primery Key Identity(1,1)


            builder.Property(x => x.Name)
                .IsRequired()//NOT NULL
                .HasColumnName("Name") //Pode Colocar o nome da coluna se for do mesmo nome não precisa colocar.
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()//NOT NULL
                .HasColumnName("Slug") //Pode Colocar o nome da coluna se for do mesmo nome não precisa colocar.
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            //Index
            builder.HasIndex(x => x.Slug, "IX_Category_Slug")
                .IsUnique();



        }
    }
}