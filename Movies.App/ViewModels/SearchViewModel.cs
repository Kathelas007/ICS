using System;
using System.Diagnostics;
using System.Windows.Input;
using Movies.App.Commands;
using Movies.BL.Interfaces;
using Movies.BL.Messages;
using Movies.BL.Models;
using Movies.BL.Repositories;
using Movies.BL.Services;

namespace Movies.App.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly Guid _moviesId;
        private readonly Guid _personsId;
        private readonly Guid _ratingsId;
        private readonly Mediator _mediator;

        public string Pattern { get; set; }
        public bool MoviesChecked { get; set; } = true;
        public bool PersonsChecked { get; set; } = true;
        public bool RatingsChecked { get; set; } = true;
        public ICommand SearchCommand { get; set; }

        public SearchViewModel(Guid movies, Guid persons, Guid ratings, Mediator mediator)
        {
            _moviesId = movies;
            _personsId = persons;
            _ratingsId = ratings;
            _mediator = mediator;

            SearchCommand = new RelayCommand(Search);

            _mediator.Register<NewMessage<MovieDetailModel>>(OnListReloaded);
            _mediator.Register<UpdateMessage<MovieDetailModel>>(OnListReloaded);
            _mediator.Register<DeleteMessage<MovieDetailModel>>(OnListReloaded);
            _mediator.Register<NewMessage<PersonDetailModel>>(OnListReloaded);
            _mediator.Register<UpdateMessage<PersonDetailModel>>(OnListReloaded);
            _mediator.Register<DeleteMessage<PersonDetailModel>>(OnListReloaded);
            _mediator.Register<NewMessage<RatingDetailModel>>(OnListReloaded);
            _mediator.Register<UpdateMessage<RatingDetailModel>>(OnListReloaded);
            _mediator.Register<DeleteMessage<RatingDetailModel>>(OnListReloaded);
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(Pattern))
            {
                Pattern = null;
            }
            SendSearchMessage(MoviesChecked, _moviesId);
            SendSearchMessage(PersonsChecked, _personsId);
            SendSearchMessage(RatingsChecked, _ratingsId);
        }

        private void OnListReloaded<T>(Message<T> message) where T : IGuid
        {
            var model = message.Model;
            if ((model is MovieDetailModel && (MoviesChecked || message is DeleteMessage<T> && RatingsChecked))
                || (model is PersonDetailModel && PersonsChecked)
                || (model is RatingDetailModel && RatingsChecked))
            {
                Pattern = null;
            }
        }

        private void SendSearchMessage(bool condition, Guid target)
        {
            var message = new SearchMessage
            {
                TargetId = target,
                Pattern = Pattern
            };
            if (!condition)
            {
                message.Pattern = null;
            }

            _mediator.Send(message);
        }
    }
}