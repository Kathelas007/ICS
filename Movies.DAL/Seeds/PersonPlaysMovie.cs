using System;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;

namespace Movies.DAL.Seeds
{
    public static class PersonPlaysMovie
    {
        public static readonly PersonPlaysMovieEntity MortensenPlaysLordOfTheRings = new PersonPlaysMovieEntity()
        {
            Id = Guid.Parse("d16de9a1-ce78-4914-89e4-2b400731a4c0"),
            ActorId = Persons.Mortensen.Id,
            MovieId = Movies.LordOfTheRings.Id
        };

        public static readonly PersonPlaysMovieEntity Mortensen2PlaysLordOfTheRings = new PersonPlaysMovieEntity()
        {
            Id = Guid.Parse("66dfc671-a6da-4332-a5ba-8564226f7510"),
            ActorId = Persons.MortensenDuplicate.Id,
            MovieId = Movies.LordOfTheRings.Id
        };

        public static readonly PersonPlaysMovieEntity MortensenPlaysLordOfTheRings2 = new PersonPlaysMovieEntity()
        {
            Id = Guid.Parse("967491f3-45bd-43f4-9553-dc2874256f79"), 
            ActorId = Persons.Mortensen.Id,
            MovieId = Movies.LordOfTheRingsDuplicate.Id
        };

        public static readonly PersonPlaysMovieEntity RedcliffPlaysHP = new PersonPlaysMovieEntity
        {
            Id = Guid.Parse("06a1e834-30fd-44d7-b9ea-3f387edcc48d"),
            ActorId = Persons.Radcliffe.Id,
            MovieId = Movies.HarryPotter.Id
        };


        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonPlaysMovieEntity>().HasData(RedcliffPlaysHP);
            modelBuilder.Entity<PersonPlaysMovieEntity>().HasData(MortensenPlaysLordOfTheRings, Mortensen2PlaysLordOfTheRings, MortensenPlaysLordOfTheRings2);
        }

    }
}