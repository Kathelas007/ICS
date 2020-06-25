using Movies.BL.Repositories;
using Movies.BL.Models;
using Movies.BL.Services;
using Movies.App.Commands;

using System.Windows.Input;
using System.Collections.ObjectModel;

using System;
using Movies.BL.Messages;
using System.Linq;
using Movies.BL.Interfaces;

namespace Movies.App.ViewModels
{
    public class RatingListViewModel : ViewModelBase
    {
        private readonly RatingRepository _repository;
        private readonly Mediator _mediator;

        public RatingListViewModel(RatingRepository repo, Mediator mediator)
        {
            _repository = repo;
            this._mediator = mediator;

            SelectCommand = new RelayCommand<RatingListModel>(Select);

            this._mediator.Register<DeleteMessage<RatingDetailModel>>(OnRatingsEdited);
            this._mediator.Register<UpdateMessage<RatingDetailModel>>(OnRatingsEdited);
            this._mediator.Register<NewMessage<RatingDetailModel>>(OnRatingsEdited);
            
            _mediator.Register<DeleteMessage<MovieDetailModel>>(OnRatingsEdited);

            _mediator.Register<SearchMessage>(OnSearch);

            Load();
        }

        public ObservableCollection<RatingListModel> Ratings { get; set; } = new ObservableCollection<RatingListModel>();

        public ICommand SelectCommand { get; set; }

     

        private void OnRatingsEdited<T>(Message<T> message) where T : IGuid
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
                foreach (var m in Ratings.Reverse())
                {
                    if (!(ContainsPatter(m.Rating, pattern) || ContainsPatter(m.Text, pattern)))
                    {
                        Ratings.Remove(m);
                    }
                }
            }
        }

        public void Select(RatingListModel rating)
        {
            RatingDetailModel ratingDetail = _repository.GetById(rating.Id);
            _mediator.Send(new SelectedMessage<MovieListModel> { TargetId = ratingDetail.Movie.Id });
        }

        public override void Load()
        {
            Ratings.Clear();
            var rating = _repository.GetAll();
            foreach (var r in rating)
            {
                Ratings.Add(r);
            }
        }

        public override void LoadInDesignMode()
        {
            var r = new RatingListModel();
            r.Rating = 8;
            r.Text = "** nejaky random hodnoceni - ujde to";
            Ratings.Add(r);
        }
    }
}