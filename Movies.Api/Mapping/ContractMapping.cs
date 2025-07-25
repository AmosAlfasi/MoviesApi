﻿using Movies.Application.Models;
using Movies.Contracts.Requests;
using Movies.Contracts.Response;
using System.Runtime.CompilerServices;

namespace Movies.Api.Mapping
{
    public static class ContractMapping
    {
        public static Movie MapToMovie(this CreateMovieRequest request)
        {
            return new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList()
            };
        }

       public static MovieResponse MapToMovieResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Slug = movie.Slug,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Genres = movie.Genres.ToList()
            };
        }

        public static MoviesResponse MapToMoviesResponse(this IEnumerable<Movie> movies)
        {
            return new MoviesResponse
            {
                Items = movies.Select(m => m.MapToMovieResponse())
            };
        }

        public static Movie MapToMovie(this UpdateMovieRequest request,Guid id)
        {
            return new Movie
            {
                Id = id,
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList()
            };
        }
    }
}
