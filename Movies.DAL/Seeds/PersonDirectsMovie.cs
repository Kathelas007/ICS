using System;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;

namespace Movies.DAL.Seeds
{
    public static class PersonDirectsMovie
    {
        public static readonly PersonDirectsMovieEntity YatesDirectsHarryPotter = new PersonDirectsMovieEntity()
        {
            Id = Guid.Parse("d184f72b-93ba-4492-8a83-d89df1555147"),
            DirectorId = Seeds.Persons.Yates.Id,
            MovieId = Seeds.Movies.HarryPotter.Id
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonDirectsMovieEntity>().HasData(YatesDirectsHarryPotter);
        }
    }
}