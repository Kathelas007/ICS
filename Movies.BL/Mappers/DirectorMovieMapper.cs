using System;

using Movies.DAL.Entities;
using Movies.BL.Models;

namespace Movies.BL.Mappers
{
    public static class DirectorMovieMapper
    {
        public static PersonDirectsMovieEntity MapToEntity(DirectorMovieModel director_movie)
        {
            if (director_movie == null) return null;

            return new PersonDirectsMovieEntity
            {
                Id = director_movie.Id,
                DirectorId = director_movie.DirectorId,
                Director = PersonMapper.MapToEntity(director_movie.Director),
                MovieId = director_movie.MovieId,
                Movie = MovieMapper.MapToEntity(director_movie.Movie)
            };
        }

        public static DirectorMovieModel MapToModel(PersonDirectsMovieEntity director_movie)
        {
            if (director_movie == null) return null;

            return new DirectorMovieModel
            {
                Id = director_movie.Id,
                DirectorId = director_movie.DirectorId,
                Director = PersonMapper.MapToList(director_movie.Director),
                MovieId = director_movie.MovieId,
                Movie = MovieMapper.MapToList(director_movie.Movie)
            };

        }

    }
}
