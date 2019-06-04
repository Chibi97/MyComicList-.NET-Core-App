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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=my_comic_list;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComicConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ComicGenreConfiguration());
            modelBuilder.ApplyConfiguration(new MyListConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new ComicAuthorConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());

            DefaultDateValue<Comic>(modelBuilder);
            DefaultDateValue<Genre>(modelBuilder);
            DefaultDateValue<User>(modelBuilder);
            DefaultDateValue<MyList>(modelBuilder);
            DefaultDateValue<ComicGenres>(modelBuilder);
            DefaultDateValue<Review>(modelBuilder);
            DefaultDateValue<Author>(modelBuilder);
            DefaultDateValue<ComicAuthors>(modelBuilder);
            DefaultDateValue<Publisher>(modelBuilder);
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
