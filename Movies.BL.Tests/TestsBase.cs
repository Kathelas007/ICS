using System;
using Movies.DAL.Factories;

namespace Movies.BL.Tests
{
    public class TestsBase : IDisposable
    {
        protected readonly DbContextInMemoryFactory dbContextFactory = new DbContextInMemoryFactory();

        public TestsBase()
        {
            using var context = dbContextFactory.Create();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            using var context = dbContextFactory.Create();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}