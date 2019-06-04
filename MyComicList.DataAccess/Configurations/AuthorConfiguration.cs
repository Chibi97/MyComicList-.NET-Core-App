using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(30).IsRequired();

            builder.Property(a => a.FullName)
                   .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.HasMany(a => a.ComicAuthors)
                    .WithOne(ca => ca.Author)
                    .HasForeignKey(ca => ca.AuthorId);
        }
    }
}
