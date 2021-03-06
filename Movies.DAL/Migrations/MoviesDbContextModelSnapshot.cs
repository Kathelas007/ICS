﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.DAL;

namespace Movies.DAL.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    partial class MoviesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Movies.DAL.Entities.MovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CzechName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7"),
                            CzechName = "Pán prstenů: Návrat krále",
                            Description = "Nadchází čas rozhodující bitvy o přežití Středozemě. Putování jednotlivých členů Společenstva prstenu se dostává do poslední a rozhodující fáze. Čaroděj Gandalf, elf Legolas a trpaslík Gimli spěchají s dědicem trůnu Aragornem na pomoc zemi Gondor, která odolává ohromnému Sauronovu vojsku. Hobiti Frodo a Sam se v doprovodu Gluma snaží dostat hluboko do země Mordor, kde musí v ohních Hory osudu zničit magický Prsten moci. Jedině tak bude síla mocného pána temnot Saurona zlomena. Podaří se jim naplnit poslání Společenstva a zachránit Středozem? A za jakou cenu?",
                            Duration = new TimeSpan(0, 3, 21, 0, 0),
                            Genre = "Fantasy, Dobrodružný, Akční",
                            OriginalName = "The Lord of the Rings: The Return of the King",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("20329257-c9b4-4344-8700-2816a2c8beb7"),
                            Country = "USA",
                            CzechName = "Harry Potter a Relikvie smrti",
                            Description = "Harry, Ron, and Hermione search for Voldemort's remaining Horcruxes in their effort to destroy the Dark Lord as the final battle rages on at Hogwarts",
                            Duration = new TimeSpan(0, 2, 10, 0, 0),
                            Genre = "Fantasy, Dobrodružný, Akční",
                            OriginalName = "Harry Potter and the Deathly Hallows",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("0c766d7b-916d-400a-a14d-8ec68ada8a1e"),
                            Country = "USA",
                            CzechName = "Pán prstenů: Návrat krále",
                            Description = "Nadchází čas rozhodující bitvy o přežití Středozemě. Putování jednotlivých členů Společenstva prstenu se dostává do poslední a rozhodující fáze. Čaroděj Gandalf, elf Legolas a trpaslík Gimli spěchají s dědicem trůnu Aragornem na pomoc zemi Gondor, která odolává ohromnému Sauronovu vojsku. Hobiti Frodo a Sam se v doprovodu Gluma snaží dostat hluboko do země Mordor, kde musí v ohních Hory osudu zničit magický Prsten moci. Jedině tak bude síla mocného pána temnot Saurona zlomena. Podaří se jim naplnit poslání Společenstva a zachránit Středozem? A za jakou cenu?",
                            Duration = new TimeSpan(0, 3, 21, 0, 0),
                            Genre = "Fantasy, Dobrodružný, Akční",
                            OriginalName = "The Lord of the Rings: The Return of the King",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("59b9cb7d-864e-4894-ab68-796f6b170ca5"),
                            Country = "USA",
                            CzechName = "Spatnej film",
                            Description = "Self describing",
                            Duration = new TimeSpan(0, 0, 10, 0, 0),
                            Genre = "Bad films",
                            OriginalName = "Bad film",
                            Photo = "Photo.png"
                        });
                });

            modelBuilder.Entity("Movies.DAL.Entities.PersonDirectsMovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DirectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.HasIndex("MovieId");

                    b.ToTable("PersonsDirectMovies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d184f72b-93ba-4492-8a83-d89df1555147"),
                            DirectorId = new Guid("b9767dba-6489-485e-bf61-4b2f2ba1a094"),
                            MovieId = new Guid("20329257-c9b4-4344-8700-2816a2c8beb7")
                        });
                });

            modelBuilder.Entity("Movies.DAL.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("481c97bb-4cbd-46ef-ab93-91d1b2e46128"),
                            Age = 61,
                            FirstName = "Viggo",
                            LastName = "Mortensen",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("b9767dba-6489-485e-bf61-4b2f2ba1a094"),
                            Age = 47,
                            FirstName = "David",
                            LastName = "Yates",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("cc767aaa-6489-485e-bf61-4bff2ba1a033"),
                            Age = 30,
                            FirstName = "Daniel",
                            LastName = "radcliffe",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("e64714bc-3eeb-4966-bb97-e7ce92daa36f"),
                            Age = 61,
                            FirstName = "Viggo",
                            LastName = "Mortensen",
                            Photo = "photo.png"
                        },
                        new
                        {
                            Id = new Guid("e52ec127-1834-4ca6-8a7a-9936de02c780"),
                            Age = 30,
                            FirstName = "Peter",
                            LastName = "NoName"
                        });
                });

            modelBuilder.Entity("Movies.DAL.Entities.PersonPlaysMovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("MovieId");

                    b.ToTable("PersonsPlayMovies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("06a1e834-30fd-44d7-b9ea-3f387edcc48d"),
                            ActorId = new Guid("cc767aaa-6489-485e-bf61-4bff2ba1a033"),
                            MovieId = new Guid("20329257-c9b4-4344-8700-2816a2c8beb7")
                        },
                        new
                        {
                            Id = new Guid("d16de9a1-ce78-4914-89e4-2b400731a4c0"),
                            ActorId = new Guid("481c97bb-4cbd-46ef-ab93-91d1b2e46128"),
                            MovieId = new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7")
                        },
                        new
                        {
                            Id = new Guid("66dfc671-a6da-4332-a5ba-8564226f7510"),
                            ActorId = new Guid("e64714bc-3eeb-4966-bb97-e7ce92daa36f"),
                            MovieId = new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7")
                        },
                        new
                        {
                            Id = new Guid("967491f3-45bd-43f4-9553-dc2874256f79"),
                            ActorId = new Guid("481c97bb-4cbd-46ef-ab93-91d1b2e46128"),
                            MovieId = new Guid("0c766d7b-916d-400a-a14d-8ec68ada8a1e")
                        });
                });

            modelBuilder.Entity("Movies.DAL.Entities.RatingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d3ea76e3-c308-4c79-af81-37e86c4b79d0"),
                            MovieId = new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7"),
                            Rating = 1000,
                            Text = "Fakt dobrý hodnocení"
                        },
                        new
                        {
                            Id = new Guid("cf50ff38-e568-4cdd-9508-3065b0370aa2"),
                            MovieId = new Guid("20329257-c9b4-4344-8700-2816a2c8beb7"),
                            Rating = 1000,
                            Text = "Chapu, ze ne tak dobry jako LOR, ale je to hned za nim."
                        },
                        new
                        {
                            Id = new Guid("f7807d79-c1e7-4f3d-a709-428f11a5787c"),
                            MovieId = new Guid("0c766d7b-916d-400a-a14d-8ec68ada8a1e"),
                            Rating = 1000,
                            Text = "Fakt dobrý hodnocení"
                        });
                });

            modelBuilder.Entity("Movies.DAL.Entities.PersonDirectsMovieEntity", b =>
                {
                    b.HasOne("Movies.DAL.Entities.PersonEntity", "Director")
                        .WithMany("DirectedMovies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.DAL.Entities.MovieEntity", "Movie")
                        .WithMany("Directors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movies.DAL.Entities.PersonPlaysMovieEntity", b =>
                {
                    b.HasOne("Movies.DAL.Entities.PersonEntity", "Actor")
                        .WithMany("PlayedMovies")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.DAL.Entities.MovieEntity", "Movie")
                        .WithMany("Actors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movies.DAL.Entities.RatingEntity", b =>
                {
                    b.HasOne("Movies.DAL.Entities.MovieEntity", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
