using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class ComicConfiguration : IEntityTypeConfiguration<Comic>
    {
        public void Configure(EntityTypeBuilder<Comic> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Description).HasMaxLength(700).IsRequired();

            builder.Property(c => c.PublishedAt).HasDefaultValueSql("NOW()");

            builder.HasMany(c => c.Users)
                .WithOne(cu => cu.Comic)
                .HasForeignKey(cu => cu.ComicId);

            builder.HasMany(c => c.ComicGenres)
                .WithOne(cu => cu.Comic)
                .HasForeignKey(cu => cu.ComicId);

            builder.HasMany(c => c.ComicAuthors)
                .WithOne(ca => ca.Comic)
                .HasForeignKey(ca => ca.ComicId);

            builder.HasOne(c => c.Publisher).WithMany(p => p.Comics);

        }
    }
}
