﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyComicList.DataAccess;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyComicList.DataAccess.Migrations
{
    [DbContext(typeof(MyComicListContext))]
    [Migration("20190531220545_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("MyComicList.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MyComicList.Domain.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Issues");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Comics");
                });

            modelBuilder.Entity("MyComicList.Domain.ComicCategories", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("ComicId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("CategoryId", "ComicId");

                    b.HasIndex("ComicId");

                    b.ToTable("ComicCategories");
                });

            modelBuilder.Entity("MyComicList.Domain.ComicUser", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ComicId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("UserId", "ComicId");

                    b.HasIndex("ComicId");

                    b.ToTable("ComicUser");
                });

            modelBuilder.Entity("MyComicList.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Username")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[FirstName.ToLower()] + '_' + [LastName.ToLower()]");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyComicList.Domain.ComicCategories", b =>
                {
                    b.HasOne("MyComicList.Domain.Category", "Category")
                        .WithMany("ComicCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyComicList.Domain.Comic", "Comic")
                        .WithMany("ComicCategories")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyComicList.Domain.ComicUser", b =>
                {
                    b.HasOne("MyComicList.Domain.Comic", "Comic")
                        .WithMany("ComicUsers")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyComicList.Domain.User", "User")
                        .WithMany("ComicUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
