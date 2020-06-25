using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Movies.BL.Interfaces;

namespace Movies.BL.Models
{
    public abstract class ModelBase : IGuid, INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
