using System;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;

namespace Movies.DAL.Seeds
{
    public static class Ratings
    {
        public static readonly RatingEntity LOTRRating = new RatingEntity()
        {
            Id = Guid.Parse("d3ea76e3-c308-4c79-af81-37e86c4b79d0"),
            Rating = 1000,
            Text = "Fakt dobrý hodnocení",
            MovieId = Movies.LordOfTheRings.Id
        };

        public static readonly RatingEntity HPRating = new RatingEntity()
        {
            Id = Guid.Parse("cf50ff38-e568-4cdd-9508-3065b0370aa2"),
            Rating = 1000,
            Text = "Chapu, ze ne tak dobry jako LOR, ale je to hned za nim.",
            MovieId = Movies.HarryPotter.Id
        };

        public static readonly RatingEntity LOTRRatingDuplicate = new RatingEntity()
        {
            Id = Guid.Parse("f7807d79-c1e7-4f3d-a709-428f11a5787c"),
            Rating = 1000,
            Text = "Fakt dobrý hodnocení",
            MovieId = Movies.LordOfTheRingsDuplicate.Id
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatingEntity>().HasData(LOTRRating);
            modelBuilder.Entity<RatingEntity>().HasData(HPRating);
            modelBuilder.Entity<RatingEntity>().HasData(LOTRRatingDuplicate);
        }
    }
}
