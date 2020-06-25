using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;

namespace Movies.DAL.Seeds
{
    public static class Movies
    {
        public static readonly MovieEntity LordOfTheRings = new MovieEntity()
        {
            Id = Guid.Parse("23488cac-1f8b-4b8f-b9b5-66672838e8e7"),
            CzechName = "Pán prstenů: Návrat krále",
            OriginalName = "The Lord of the Rings: The Return of the King",
            Description =
                "Nadchází čas rozhodující bitvy o přežití Středozemě. Putování jednotlivých členů Společenstva prstenu se dostává do poslední a rozhodující fáze. Čaroděj Gandalf, elf Legolas a trpaslík Gimli spěchají s dědicem trůnu Aragornem na pomoc zemi Gondor, která odolává ohromnému Sauronovu vojsku. Hobiti Frodo a Sam se v doprovodu Gluma snaží dostat hluboko do země Mordor, kde musí v ohních Hory osudu zničit magický Prsten moci. Jedině tak bude síla mocného pána temnot Saurona zlomena. Podaří se jim naplnit poslání Společenstva a zachránit Středozem? A za jakou cenu?",
            Duration = new TimeSpan(hours: 0, minutes: 201, seconds: 0),
            Genre = "Fantasy, Dobrodružný, Akční",
            Photo = "photo.png"
        };

        public static readonly MovieEntity HarryPotter = new MovieEntity()
        {
            Id = Guid.Parse("20329257-c9b4-4344-8700-2816a2c8beb7"),
            CzechName = "Harry Potter a Relikvie smrti",
            OriginalName = "Harry Potter and the Deathly Hallows",
            Description =
                "Harry, Ron, and Hermione search for Voldemort's remaining Horcruxes in their effort to destroy the Dark Lord as the final battle rages on at Hogwarts",
            Duration = new TimeSpan(hours: 2, minutes: 10, seconds: 0),
            Genre = "Fantasy, Dobrodružný, Akční",
            Photo = "photo.png",
            Country = "USA"

        };

        public static readonly MovieEntity LordOfTheRingsDuplicate = new MovieEntity()
        {
            Id = Guid.Parse("0c766d7b-916d-400a-a14d-8ec68ada8a1e"),
            CzechName = "Pán prstenů: Návrat krále",
            OriginalName = "The Lord of the Rings: The Return of the King",
            Description =
                "Nadchází čas rozhodující bitvy o přežití Středozemě. Putování jednotlivých členů Společenstva prstenu se dostává do poslední a rozhodující fáze. Čaroděj Gandalf, elf Legolas a trpaslík Gimli spěchají s dědicem trůnu Aragornem na pomoc zemi Gondor, která odolává ohromnému Sauronovu vojsku. Hobiti Frodo a Sam se v doprovodu Gluma snaží dostat hluboko do země Mordor, kde musí v ohních Hory osudu zničit magický Prsten moci. Jedině tak bude síla mocného pána temnot Saurona zlomena. Podaří se jim naplnit poslání Společenstva a zachránit Středozem? A za jakou cenu?",
            Duration = new TimeSpan(hours: 0, minutes: 201, seconds: 0),
            Genre = "Fantasy, Dobrodružný, Akční",
            Photo = "photo.png",
            Country = "USA"
        };

        public static readonly MovieEntity NoNameFilm = new MovieEntity()
        {
            Id = Guid.Parse("59b9cb7d-864e-4894-ab68-796f6b170ca5"),
            CzechName = "Spatnej film",
            OriginalName = "Bad film",
            Description = "Self describing",
            Duration = new TimeSpan(hours: 0, minutes: 10, seconds: 0),
            Genre = "Bad films",
            Photo = "Photo.png",
            Country = "USA"
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieEntity>().HasData(LordOfTheRings);
            modelBuilder.Entity<MovieEntity>().HasData(HarryPotter);
            modelBuilder.Entity<MovieEntity>().HasData(LordOfTheRingsDuplicate);
            modelBuilder.Entity<MovieEntity>().HasData(NoNameFilm);
        }
    }
}