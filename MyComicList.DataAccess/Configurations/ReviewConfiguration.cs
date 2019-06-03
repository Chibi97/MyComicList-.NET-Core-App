using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Title).HasMaxLength(30);
            builder.Property(r => r.Text).HasMaxLength(500);
            builder.Property(r => r.Rating).IsRequired();

            builder.HasOne(r => r.User).WithMany(u => u.Reviews);
            builder.HasOne(r => r.Comic).WithMany(c => c.Reviews);

        }
    }
}
