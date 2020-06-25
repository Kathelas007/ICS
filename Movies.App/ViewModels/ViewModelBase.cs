using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Movies.BL.Interfaces;

namespace Movies.App.ViewModels
{
    public class ViewModelBase : IViewModel, IGuid, INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ViewModelBase()
        {
            Id = Guid.NewGuid();
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                LoadInDesignMode();
            }
        }

        public virtual void LoadInDesignMode()
        {

        }

        public virtual void Load()
        {

        }

        protected bool ContainsPatter(string str, string pattern) {
            str = str.ToLower();
            pattern = pattern.ToLower();
            return str.Contains(pattern);
        }

        protected bool ContainsPatter(int num, string pattern) {
            var str = num.ToString();
            return str == pattern;
        }
    }
}