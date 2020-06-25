using System;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;

namespace Movies.DAL.Seeds
{
    public static class Persons
    {
        public static readonly PersonEntity Mortensen = new PersonEntity()
        {
            Id = Guid.Parse("481c97bb-4cbd-46ef-ab93-91d1b2e46128"),
            FirstName = "Viggo",
            LastName = "Mortensen",
            Age = 61,
            Photo = "photo.png"

        };

        public static readonly PersonEntity Yates = new PersonEntity()
        {
            Id = Guid.Parse("b9767dba-6489-485e-bf61-4b2f2ba1a094"),
            FirstName = "David",
            LastName = "Yates",
            Age = 47,
            Photo = "photo.png",
        };

        public static readonly PersonEntity Radcliffe = new PersonEntity()
        {
            Id = Guid.Parse("cc767aaa-6489-485e-bf61-4bff2ba1a033"),
            FirstName = "Daniel",
            LastName = "radcliffe",
            Age = 30,
            Photo = "photo.png"
        };

        public static readonly PersonEntity MortensenDuplicate = new PersonEntity()
        {
            Id = Guid.Parse("e64714bc-3eeb-4966-bb97-e7ce92daa36f"),
            FirstName = "Viggo",
            LastName = "Mortensen",
            Age = 61,
            Photo = "photo.png"
        };

        public static readonly PersonEntity NoNameActor = new PersonEntity()
        {
            Id = Guid.Parse("e52ec127-1834-4ca6-8a7a-9936de02c780"),
            FirstName = "Peter",
            LastName = "NoName",
            Age = 30
        };

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>().HasData(Mortensen);
            modelBuilder.Entity<PersonEntity>().HasData(Yates);
            modelBuilder.Entity<PersonEntity>().HasData(Radcliffe);
            modelBuilder.Entity<PersonEntity>().HasData(MortensenDuplicate);
            modelBuilder.Entity<PersonEntity>().HasData(NoNameActor);
            
        }
    }
}