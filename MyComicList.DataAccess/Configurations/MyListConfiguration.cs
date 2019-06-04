using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class MyListConfiguration : IEntityTypeConfiguration<MyList>
    {
        public void Configure(EntityTypeBuilder<MyList> builder)
        {
            builder.HasKey(cu => new { cu.UserId, cu.ComicId });
        }
    }
}
