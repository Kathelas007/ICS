using System;

namespace Movies.DAL.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}