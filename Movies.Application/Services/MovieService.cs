using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<Movie> _movieValidator;
        private readonly IRatingRepository _ratingRepository;

        public MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator,IRatingRepository ratingRepository) 
        {
            _movieRepository = movieRepository;
            _movieValidator = movieValidator;
            _ratingRepository = ratingRepository;
        }
        public async Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: cancellationToken);
            return await _movieRepository.CreateAsync(movie, cancellationToken);
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _movieRepository.DeleteByIdAsync(id, cancellationToken);
        }

        public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _movieRepository.ExistsByIdAsync(id, cancellationToken);    
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(Guid? userId, CancellationToken cancellationToken = default)
        {
            return await _movieRepository.GetAllAsync(userId, cancellationToken);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, Guid? userId, CancellationToken cancellationToken = default)
        {
           return await _movieRepository.GetByIdAsync(id, userId, cancellationToken);
        }

        public async Task<Movie?> GetBySlugAsync(string slug, Guid? userId, CancellationToken cancellationToken = default)
        {
           return await _movieRepository.GetBySlugAsync(slug, userId, cancellationToken);
        }

        public async Task<Movie?> UpdateAsync(Movie movie, Guid? userId, CancellationToken cancellationToken = default)
        {
            await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: cancellationToken);
            var movieExist = await _movieRepository.ExistsByIdAsync(movie.Id, cancellationToken);
            if (!movieExist)
            {
                return null;
            }

            await _movieRepository.UpdateAsync(movie, cancellationToken);

            if (!userId.HasValue)
            {
                var rating = _ratingRepository.GetRatingAsync(movie.Id, cancellationToken);
                return movie;
            }
            var ratings =await _ratingRepository.GetRatingAsync(movie.Id,userId.Value, cancellationToken);
            movie.Rating = ratings.rating;
            movie.UserRating = ratings.userRating;
            return movie;
        }
    }
}
