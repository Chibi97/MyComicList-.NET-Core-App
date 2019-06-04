using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.Property(r => r.Title).HasMaxLength(50);
            builder.Property(r => r.Text).HasMaxLength(500);
            builder.Property(r => r.Rating).IsRequired();
        }
    }
}
