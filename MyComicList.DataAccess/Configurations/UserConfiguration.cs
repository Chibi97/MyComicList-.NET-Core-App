using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30).IsRequired();

            builder.Property(u => u.Username)
                   .HasComputedColumnSql("[FirstName.ToLower()] + '_' + [LastName.ToLower()]");

            builder.HasIndex(u => u.Email).IsUnique();

            //builder.HasOne(u => u.Role).WithMany(r => r.Users); one-to-many

            builder.HasMany(u => u.ComicUsers)
                    .WithOne(cu => cu.User)
                    .HasForeignKey(cu => cu.UserId);

        }
    }
}
