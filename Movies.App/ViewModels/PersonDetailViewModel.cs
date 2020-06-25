using Movies.BL.Repositories;
using Movies.BL.Models;
using Movies.DAL.Interfaces;
using Movies.BL.Services;
using Movies.App.Commands;
using Movies.BL.Mappers;

using System.Windows.Input;
using System.Collections.ObjectModel;

using System.Collections.Generic;
using System;
using Movies.BL.Messages;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Movies.App.ViewModels
{
    public class PersonDetailViewModel : ViewModelBase
    {
        private readonly PersonRepository _repository;
        private readonly Mediator _mediator;

        public PersonDetailViewModel(PersonRepository repo, Mediator mediator)
        {
            _repository = repo;
            this._mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);

            // register commands

            this._mediator.Register<SelectedMessage<PersonListModel>>(OnPersonSelected);
            this._mediator.Register<NewMessage<PersonListModel>>(OnPersonNew);
        }

        public PersonDetailModel Person { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        private void OnPersonSelected(SelectedMessage<PersonListModel> message) {
            if (message.TargetId != Guid.Empty)
            {
                Person = _repository.GetById(message.TargetId);
            }
        }

        private void OnPersonNew(NewMessage<PersonListModel> message)
        {
            Person = new PersonDetailModel();
        }

        public void Save()
        {
            Person = _repository.InsertOrUpdate(Person);
            Id = Person.Id;
            _mediator.Send(new NewMessage<PersonDetailModel>
            {
                Model = Person
            });
        }

        public bool CanSave()
        {
            if (Person != null &&
                !string.IsNullOrEmpty(Person.FirstName) && !string.IsNullOrWhiteSpace(Person.FirstName) &&
                !string.IsNullOrEmpty(Person.LastName) && !string.IsNullOrWhiteSpace(Person.LastName))
            {
                return true;
            }
            return false;
        }

        public void Delete()
        {
            if (Person.Id == Guid.Empty) return;

            _repository.Delete(Person.Id);
            _mediator.Send(new DeleteMessage<PersonDetailModel> { Model =Person });

            Id = Guid.Empty;
            Person = null;
        }

        public override void LoadInDesignMode()
        {
            Person = new PersonDetailModel();
            Person.Age = 25;
            Person.FirstName = "Johnny";
            Person.LastName = "Depp";
            Person.Photo = "photo.png";
            Person.Age = 56;

            var m = new MovieListModel();
            m.OriginalName = "Pirates of Carribean";
            m.CzechName = "Pirati z Karibiku";

            var am = new ActorMovieModel();
            am.Movie = m;

            Person.PlayedMovies = new[] { am };
        }
    }
}