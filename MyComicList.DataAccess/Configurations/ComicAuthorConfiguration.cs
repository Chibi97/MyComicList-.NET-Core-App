using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class ComicAuthorConfiguration : IEntityTypeConfiguration<ComicAuthors>
    {
        public void Configure(EntityTypeBuilder<ComicAuthors> builder)
        {
            builder.HasKey(ca => new { ca.AuthorId, ca.ComicId });
        }
    }
}
