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
using Movies.DAL.Factories;

namespace Movies.App.ViewModels
{
    public class RatingDetailViewModel : ViewModelBase
    {
        private readonly RatingRepository _repository;
        private readonly Mediator _mediator;

        public RatingDetailViewModel(RatingRepository repo, Mediator mediator)
        {
            _repository = repo;
            this._mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);

            this._mediator.Register<SelectedMessage<RatingListModel>>(OnRatingSelected);
            this._mediator.Register<NewMessage<MovieDetailModel>>(OnRatingNew);
        }

        public RatingDetailModel Rating { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnRatingSelected(SelectedMessage<RatingListModel> message)
        {
            if (message.TargetId != Guid.Empty)
            {
                Rating = _repository.GetById(message.TargetId);
            }
        }

        public void Save()
        {
            Rating = _repository.InsertOrUpdate(Rating);
            _mediator.Send(new NewMessage<RatingDetailModel> {Model = Rating});
            Rating = null;
        }

        public bool CanSave()
        {
            if (Rating != null && Rating.Movie != null &&
                !string.IsNullOrEmpty(Rating.Text) && !string.IsNullOrWhiteSpace(Rating.Text))
            {
                return true;
            }
            return false;
        }

        public void Delete() {
            if (Rating.Id == Guid.Empty) return;

            _repository.Delete(Rating.Id);
            _mediator.Send(new DeleteMessage<RatingDetailModel> { Model = Rating });

            Id = Guid.Empty;
            Rating = null;

        }

        public void OnRatingNew( NewMessage<MovieDetailModel> message)
        {
            Rating = new RatingDetailModel();
            MovieRepository repo = new MovieRepository(new DbContextRunTimeFactory());
           var movies = repo.GetAll();
           foreach (var m in movies)
           {
               if (m.Id == message.Model.Id)
               {
                   Rating.Movie = m;
                   break;
               }
           }
        }

        public override void LoadInDesignMode()
        {
            Rating = new RatingDetailModel();
            Rating.Rating = 8;
            Rating.Text = "Dobry to je";

            var m = new MovieListModel();
            m.OriginalName = "Zorba the Greek";
            m.CzechName = "Rek Zorba";

            Rating.Movie = m;
        }
    }
}