using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.DataAccess.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        }
    }
}
