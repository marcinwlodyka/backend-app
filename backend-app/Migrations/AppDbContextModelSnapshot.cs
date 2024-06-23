﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend_app.Data;

#nullable disable

namespace backend_app.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<int>("ActorsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActorsId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("ActorMovie");

                    b.HasData(
                        new
                        {
                            ActorsId = 1,
                            MoviesId = 1
                        },
                        new
                        {
                            ActorsId = 2,
                            MoviesId = 1
                        },
                        new
                        {
                            ActorsId = 3,
                            MoviesId = 2
                        },
                        new
                        {
                            ActorsId = 4,
                            MoviesId = 3
                        });
                });

            modelBuilder.Entity("backend_app.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1975, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Tobey",
                            LastName = "Maguire"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1955, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Willem",
                            LastName = "Dafoe"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1989, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Daniel",
                            LastName = "Radcliffe"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1974, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Leonardo",
                            LastName = "DiCaprio"
                        });
                });

            modelBuilder.Entity("backend_app.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "💥🔫",
                            Name = "action"
                        },
                        new
                        {
                            Id = 2,
                            Description = "🧙🪄️",
                            Name = "fantasy"
                        },
                        new
                        {
                            Id = 3,
                            Description = "😘💘",
                            Name = "romance"
                        });
                });

            modelBuilder.Entity("backend_app.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 1,
                            ReleaseDate = new DateTime(2002, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Spider-man"
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 2,
                            ReleaseDate = new DateTime(2001, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter"
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 3,
                            ReleaseDate = new DateTime(1997, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Titanic"
                        });
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("backend_app.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_app.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend_app.Models.Movie", b =>
                {
                    b.HasOne("backend_app.Models.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("backend_app.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
