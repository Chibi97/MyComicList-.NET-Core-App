using Microsoft.EntityFrameworkCore;
using MyComicList.DataAccess.Configurations;
using MyComicList.Domain;
using MyComicList.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyComicList.DataAccess
{
    public class MyComicListContext : DbContext 
    {
        public DbSet<Comic> Comics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=my_comic_list;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComicConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ComicCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ComicUserConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

            DefaultDateValue<Comic>(modelBuilder);
            DefaultDateValue<Category>(modelBuilder);
            DefaultDateValue<User>(modelBuilder);
            DefaultDateValue<ComicUsers>(modelBuilder);
            DefaultDateValue<ComicCategories>(modelBuilder);
            DefaultDateValue<Review>(modelBuilder);
        }

        private void DefaultDateValue<T>(ModelBuilder modelBuilder)
            where T : HistoryTracker
        {
            string timeFunction = "NOW()";
            modelBuilder.Entity<T>().Property(h => h.CreatedAt)
                        .HasDefaultValueSql(timeFunction);

            modelBuilder.Entity<T>().Property(h => h.UpdatedAt)
                        .HasDefaultValueSql(timeFunction);
        }
    }
}
