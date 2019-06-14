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
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(20).IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();

            builder.HasOne(u => u.Role).WithMany(r => r.Users);

            builder.HasMany(u => u.Comics)
                    .WithOne(cu => cu.User)
                    .HasForeignKey(cu => cu.UserId);

            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

        }
    }
}
