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
    public class ComicCategoryConfiguration : IEntityTypeConfiguration<ComicCategories>
    {
        public void Configure(EntityTypeBuilder<ComicCategories> builder)
        {
            builder.HasKey(cc => new { cc.CategoryId, cc.ComicId });
        }
    }
}
