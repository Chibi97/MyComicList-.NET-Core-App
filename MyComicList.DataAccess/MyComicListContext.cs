using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Role> Roles { get; set; }

        IConfiguration config { get; set; }
        public MyComicListContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(config.GetSection("Database")["ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefaultDateValue<Comic>(modelBuilder);
            DefaultDateValue<Genre>(modelBuilder);
            DefaultDateValue<User>(modelBuilder);
            DefaultDateValue<Role>(modelBuilder);
            DefaultDateValue<Publisher>(modelBuilder);
            DefaultDateValue<Author>(modelBuilder);
            DefaultDateValue<MyList>(modelBuilder);
            DefaultDateValue<Review>(modelBuilder);
            DefaultDateValue<ComicGenres>(modelBuilder);
            DefaultDateValue<ComicAuthors>(modelBuilder);

            modelBuilder.ApplyConfiguration(new ComicConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new MyListConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ComicGenreConfiguration());
            modelBuilder.ApplyConfiguration(new ComicAuthorConfiguration());
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
