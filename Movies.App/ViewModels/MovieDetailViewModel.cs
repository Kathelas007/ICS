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
using System.ComponentModel;
using Movies.BL.Messages;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Movies.App.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private readonly MovieRepository _repository;
        private readonly Mediator _mediator;

        public MovieDetailViewModel(MovieRepository repo, Mediator mediator)
        {
            _repository = repo;
            this._mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
            NewRatingCommand = new RelayCommand(NewRating);
            SelectRatingCommand = new RelayCommand<RatingListModel>(SelectRating);

            this._mediator.Register<NewMessage<RatingDetailModel>>(OnRatingNew);
            this._mediator.Register<DeleteMessage<RatingDetailModel>>(OnRatingDeleted);
            this._mediator.Register<UpdateMessage<RatingDetailModel>>(OnRatingUpdated);

            this._mediator.Register<SelectedMessage<MovieListModel>>(OnMovieSelected);
            this._mediator.Register<NewMessage<MovieListModel>>(OnMovieNew);
        }

        public MovieDetailModel Movie { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand NewRatingCommand { get; set; }

        public ICommand SelectRatingCommand { get; set; }

        private void OnMovieSelected(SelectedMessage<MovieListModel> message)
        {
            if (message.TargetId != Guid.Empty) {
                Movie = _repository.GetById(message.TargetId);
            }
        }

        private void OnRatingNew(NewMessage<RatingDetailModel> message) 
        {
            LoadRatings();
        }

        private void OnRatingDeleted(DeleteMessage<RatingDetailModel> message)
        {
            LoadRatings();
        }

        private void OnRatingUpdated(UpdateMessage<RatingDetailModel> message)
        {
            if (message.TargetId == Id)
            {
                List<RatingListModel> newList = Movie.Ratings.ToList();
                var r = RatingMapper.MapToList(message.Model);
                var index = newList.FindIndex(x => x.Id == r.Id);
                newList[index] = r;
                Movie.Ratings = newList;
            }
        }
        private void LoadRatings()
        {
            var movie = _repository.GetById(Movie.Id);
            Movie.Ratings = movie.Ratings;
        }

        private void OnMovieNew(NewMessage<MovieListModel> message)
        {
            Movie = new MovieDetailModel();
        }

        public void Save()
        {
            Movie = _repository.InsertOrUpdate(Movie);
            Id = Movie.Id;

            _mediator.Send(new NewMessage<MovieDetailModel>
            {
                Model = Movie
            });
        }

        public bool CanSave()
        {
            if (Movie != null &&
                ((!string.IsNullOrEmpty(Movie.OriginalName) && !string.IsNullOrWhiteSpace(Movie.OriginalName)) ||
                (!string.IsNullOrEmpty(Movie.OriginalName) && !string.IsNullOrWhiteSpace(Movie.OriginalName))))
            {
                return true;
            }
            return false;
        }

        public void Delete()
        {
            if (Movie.Id == Guid.Empty) return;

            _repository.Delete(Movie.Id);
            _mediator.Send(new DeleteMessage<MovieDetailModel> { Model = Movie });

            Id = Guid.Empty;
            Movie = null;
        }

        public void NewRating()
        {
            _mediator.Send(new NewMessage<MovieDetailModel> {Model = Movie});
        }

        public void SelectRating(RatingListModel rating)
        {
            _mediator.Send(new SelectedMessage<RatingListModel>{TargetId = rating.Id});
        }

        public override void LoadInDesignMode()
        {
            var movie = new MovieDetailModel();
            movie.CzechName = "Limonádový Joe";
            movie.OriginalName = "Limonádový Joe";
            movie.Description = "Padouch nebo hrdina, vždyť jsme jedna rodina.";
            movie.Country = "CZ";

            var d = new PersonListModel();
            d.FirstName = "Oldřich";
            d.LastName = "Lipský";

            var t = new DirectorMovieModel();
            t.Director = d;

            movie.Directors = new[] { t };

            var rating = new RatingListModel() { Rating = 4, Text = "Dobry"};
            movie.Ratings = new[] { rating };

        }
    }
}