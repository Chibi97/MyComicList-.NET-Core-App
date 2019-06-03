using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;

namespace MyComicList.DataAccess.Configurations
{
    public class ComicUserConfiguration : IEntityTypeConfiguration<ComicUsers>
    {
        public void Configure(EntityTypeBuilder<ComicUsers> builder)
        {
            builder.HasKey(cu => new { cu.UserId, cu.ComicId });
        }
    }
}
