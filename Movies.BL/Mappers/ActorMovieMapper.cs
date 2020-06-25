using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Movies.DAL.Entities;
using Movies.BL.Models;


namespace Movies.BL.Mappers
{
    public static class ActorMovieMapper
    {
        public static PersonPlaysMovieEntity MapToEntity(ActorMovieModel actor_movie)
        {
            if (actor_movie == null) return null;

            return new PersonPlaysMovieEntity
            {
                Id = actor_movie.Id,
                ActorId = actor_movie.ActorId,
                Actor = PersonMapper.MapToEntity(actor_movie.Actor),
                MovieId = actor_movie.MovieId,
                Movie = MovieMapper.MapToEntity(actor_movie.Movie)
            };
        }

        public static ActorMovieModel MapToModel(PersonPlaysMovieEntity actor_movie)
        {
            if (actor_movie == null) return null;

            return new ActorMovieModel
            {
                Id = actor_movie.Id,
                ActorId = actor_movie.ActorId,
                Actor = PersonMapper.MapToList(actor_movie.Actor),
                MovieId = actor_movie.MovieId,
                Movie = MovieMapper.MapToList(actor_movie.Movie)
            };
        }
    }
}
