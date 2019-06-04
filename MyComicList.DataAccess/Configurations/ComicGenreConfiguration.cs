using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class ComicGenreConfiguration : IEntityTypeConfiguration<ComicGenres>
    {
        public void Configure(EntityTypeBuilder<ComicGenres> builder)
        {
            builder.HasKey(cc => new { cc.GenreId, cc.ComicId });
        }
    }
}
