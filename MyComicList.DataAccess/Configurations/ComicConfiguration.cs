using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComicList.DataAccess.Configurations
{
    public class ComicConfiguration : IEntityTypeConfiguration<Comic>
    { 

    public void Configure(EntityTypeBuilder<Comic> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Description).HasMaxLength(200).IsRequired();

            builder.HasMany(c => c.ComicUsers)
                .WithOne(cu => cu.Comic)
                .HasForeignKey(cu => cu.ComicId);

            builder.HasMany(c => c.ComicCategories)
                .WithOne(cu => cu.Comic)
                .HasForeignKey(cu => cu.ComicId);

        }
    }
}
