using System;
using Movies.BL.Interfaces;

namespace Movies.BL.Messages
{
    public class SearchMessage : IMessage
    {
        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public string Pattern { get; set; }
    }
}