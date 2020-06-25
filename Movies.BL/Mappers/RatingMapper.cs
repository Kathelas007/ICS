using System;
using System.Collections.Generic;
using System.Text;

using Movies.DAL.Entities;
using Movies.BL.Models;
using Movies.BL.Mappers;

namespace Movies.BL.Mappers
{
    public static class RatingMapper
    {
        public static RatingListModel MapToList(RatingDetailModel rating)
        {
            if (rating == null) return null;

            return new RatingListModel
            {
                Id = rating.Id,
                Text = rating.Text,
                Rating = rating.Rating
            };
        }

        public static RatingListModel MapToList(RatingEntity rating)
        {
            if (rating == null) return null;

            return new RatingListModel
            {
                Id = rating.Id,
                Text = rating.Text,
                Rating = rating.Rating
            };
        }

        public static RatingDetailModel MapToDetail(RatingEntity rating)
        {
            if (rating == null) return null;

            return new RatingDetailModel
            {
                Id = rating.Id,
                Movie = MovieMapper.MapToList(rating.Movie),
                Rating = rating.Rating,
                Text = rating.Text
            };
        }

        public static RatingEntity MapToEntity(RatingDetailModel rating)
        {
            if (rating == null) return null;

            MovieEntity movie = MovieMapper.MapToEntity(rating.Movie);            

            RatingEntity entity = new RatingEntity()
            {
                Id = rating.Id,
                Movie = movie,
                Rating = rating.Rating,
                Text = rating.Text
            };

            if (movie != null) { entity.MovieId = movie.Id; }

            return entity;
        }
    }
}
