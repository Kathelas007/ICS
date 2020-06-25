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
    public class MovieListViewModel: ViewModelBase
    {
        private readonly MovieRepository _repository;
        private readonly Mediator _mediator;

        public MovieListViewModel(MovieRepository repo, Mediator mediator)
        {
            _repository = repo;
            this._mediator = mediator;

            NewCommand = new RelayCommand(New);
            SelectCommand = new RelayCommand<MovieListModel>(Select);

            this._mediator.Register<DeleteMessage<MovieDetailModel>>(OnMovieDeleted);
            this._mediator.Register<UpdateMessage<MovieDetailModel>>(OnMovieUpdated);
            this._mediator.Register<NewMessage<MovieDetailModel>>(OnMovieAdded);
            _mediator.Register<SearchMessage>(OnSearch);
        }

        public ObservableCollection<MovieListModel> Movie { get; set; } = new ObservableCollection<MovieListModel>();

        public ICommand NewCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        private void OnMovieUpdated(UpdateMessage<MovieDetailModel> message)
        {
            Load();
        }

        private void OnMovieDeleted(DeleteMessage<MovieDetailModel> message)
        {
            Load();
        }

        private void OnMovieAdded(NewMessage<MovieDetailModel> message)
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
                foreach (var m in Movie.Reverse())
                {
                    if (!ContainsPatter(m.OriginalName, pattern))
                    {
                        Movie.Remove(m);
                    }
                }
            }
        }

        public void New()
        {
            _mediator.Send(new NewMessage<MovieListModel>());
        }

        public void Select(MovieListModel movie)
        {
            _mediator.Send(new SelectedMessage<MovieListModel> { TargetId = movie.Id });
        }

        public override void Load()
        { 
            Movie.Clear();
            var movie = _repository.GetAll();
            foreach (var m in movie)
            {
                Movie.Add(m);
            }
        }

        public override void LoadInDesignMode()
        {
            var m = new MovieListModel();
            m.OriginalName = "Harry Potter";
            m.CzechName = "Harry Potter";
            m.Description = "Bylo nebylo ...";
            m.Country = "USA";

            Movie.Add(m);
        }
    }
}