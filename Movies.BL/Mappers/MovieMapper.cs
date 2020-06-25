using System.Linq;
using System.Collections.Generic;
using Movies.DAL.Entities;
using Movies.BL.Models;

using Movies.BL.Mappers;

namespace Movies.BL.Mappers
{
    public static class MovieMapper
    {
        public static MovieListModel MapToList(MovieEntity movie)
        {
            if (movie != null)
            {
                return new MovieListModel
                {
                    Id = movie.Id,
                    OriginalName = movie.OriginalName,
                    CzechName = movie.CzechName,
                    Description = movie.Description,
                    Country = movie.Country

                };
            }
            else return null;
        }

        public static MovieListModel MapToList(MovieDetailModel movie)
        {
            if (movie != null)
            {
                return new MovieListModel
                {
                    Id = movie.Id,
                    OriginalName = movie.OriginalName,
                    CzechName = movie.CzechName,
                    Description = movie.Description,
                    Country = movie.Country
                };
            }
            else return null;
        }

        public static MovieDetailModel MapToDetail(MovieEntity movie)
        {
            if (movie != null)
            {
                return new MovieDetailModel
                {
                    Id = movie.Id,
                    OriginalName = movie.OriginalName,
                    CzechName = movie.CzechName,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Country = movie.Country,

                    Actors = movie.Actors.Select(sl_actor => ActorMovieMapper.MapToModel(sl_actor)).ToList(),
                    Directors = movie.Directors.Select(sl_actor => DirectorMovieMapper.MapToModel(sl_actor)).ToList(),

                    Genre = movie.Genre,
                    Photo = movie.Photo,

                    Ratings = movie.Ratings.Select(sl_rating => RatingMapper.MapToList(sl_rating)).ToList()

                };
            }
            else return null;
        }

        public static MovieEntity MapToEntity(MovieDetailModel movie)
        {
            if (movie == null) return null;

            return new MovieEntity
            {
                Id = movie.Id,
                CzechName = movie.CzechName,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                Duration = movie.Duration,
                Country = movie.Country,
                Genre = movie.Genre,
                Photo = movie.Photo,
                Actors = movie.Actors.Select(sl_actor => ActorMovieMapper.MapToEntity(sl_actor)).ToList(),
                Directors = movie.Directors.Select(sl_actor => DirectorMovieMapper.MapToEntity(sl_actor)).ToList()
            };
        }

        public static MovieEntity MapToEntity(MovieListModel movie)
        {
            if (movie == null) return null;

            return new MovieEntity
            {
                Id = movie.Id,
                CzechName = movie.CzechName,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                Country = movie.Country
            };
        }
    }
}
