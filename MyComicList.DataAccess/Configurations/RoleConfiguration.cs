﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyComicList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyComicList.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}