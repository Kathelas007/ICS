using System;

namespace Movies.BL.Interfaces
{
    public interface IMessage
    {
        Guid Id { get; set; }
        Guid TargetId { get; set; }
    }
}