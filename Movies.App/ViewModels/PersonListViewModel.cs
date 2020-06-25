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
    public class PersonListViewModel: ViewModelBase
    {

        private readonly PersonRepository _repository;
        private readonly Mediator _mediator;

        public PersonListViewModel(PersonRepository repo, Mediator mediator)
        {
            _repository = repo;
            this._mediator = mediator;

            NewCommand = new RelayCommand(New);
            SelectCommand = new RelayCommand<PersonListModel>(Select);

            this._mediator.Register<DeleteMessage<PersonDetailModel>>(OnPersonDeleted);
            this._mediator.Register<UpdateMessage<PersonDetailModel>>(OnPersonUpdated);
            this._mediator.Register<NewMessage<PersonDetailModel>>(OnPersonAdded);
            _mediator.Register<SearchMessage>(OnSearch);

            Load();
        }

        public ObservableCollection<PersonListModel> Persons { get; set; } = new ObservableCollection<PersonListModel>();

        public ICommand NewCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        private void OnPersonUpdated(UpdateMessage<PersonDetailModel> message)
        {
            Load();
        }

        private void OnPersonDeleted(DeleteMessage<PersonDetailModel> message)
        {
            Load();
        }

        private void OnPersonAdded(NewMessage<PersonDetailModel> message)
        {
            Load();
        }

        private void OnSearch(SearchMessage message)
        {
            if (message.TargetId != Id) return;

            Load();

            var pattern = message.Pattern;
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                foreach (var m in Persons.Reverse())
                {
                    if (!(ContainsPatter(m.FirstName, pattern) || ContainsPatter(m.LastName, pattern)))
                    {
                        Persons.Remove(m);
                    }
                }
            }
        }

        public void New()
        {
            _mediator.Send(new NewMessage<PersonListModel>());
        }

        public void Select(PersonListModel person)
        {
            _mediator.Send(new SelectedMessage<PersonListModel> { TargetId = person.Id });
        }

        public override void Load()
        {
            Persons.Clear();
            var persons = _repository.GetAll();
            foreach (var p in persons)
            {
                Persons.Add(p);
            }
        }

        public override void LoadInDesignMode()
        {
            var p = new PersonListModel();
            p.FirstName = "David";
            p.LastName = "Redcliff";
            Persons.Add(p);

            p.FirstName = "Chuck";
            p.LastName = "Norris";
            Persons.Add(p);
        }
    }
}





