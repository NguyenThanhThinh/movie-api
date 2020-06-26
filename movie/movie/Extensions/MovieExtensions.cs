﻿using movie.Data.Entities;

namespace movie.Extensions
{
    public static class MovieExtensions
    {
        public static void Map(this Movie dbMovie, Movie movie)
        {
            dbMovie.Name = movie.Name;
            dbMovie.Genre = movie.Genre;
            dbMovie.Director = movie.Director;
        }
    }
}
