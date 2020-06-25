using System;
using System.Security.Principal;
using Movies.BL.Interfaces;

namespace Movies.BL.Messages
{
    public abstract class Message<T> : IMessage where T : IGuid
    {
        private Guid? _id;
        public Guid Id
        {
            get => _id ?? Model.Id;
            set => _id = value;
        }
        public Guid TargetId { get; set; }
        public T Model { get; set; }
    }

    public class NewMessage<T> : Message<T> where T : IGuid
    {
    }
    
    public class DeleteMessage<T> : Message<T> where T : IGuid
    {
    }

    public class SelectedMessage<T> : Message<T> where T : IGuid
    {
    }

    public class UpdateMessage<T> : Message<T> where T : IGuid
    {
    }
}