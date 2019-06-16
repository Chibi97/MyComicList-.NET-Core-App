using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            //builder.HasIndex(c => c.Name).IsUnique();

            builder.HasMany(c => c.ComicGenres)
                    .WithOne(cc => cc.Genre)
                    .HasForeignKey(cc => cc.GenreId);
        }
    }
}
