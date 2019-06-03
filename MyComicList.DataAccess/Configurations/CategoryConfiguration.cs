using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasMany(c => c.ComicCategories)
                    .WithOne(cc => cc.Category)
                    .HasForeignKey(cc => cc.CategoryId);

        }

    }
}
