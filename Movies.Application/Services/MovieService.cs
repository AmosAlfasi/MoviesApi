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

        public MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator) 
        {
            _movieRepository = movieRepository;
            _movieValidator = movieValidator;
        }
        public async Task<bool> CreateAsync(Movie movie)
        {
            await _movieValidator.ValidateAndThrowAsync(movie);
            return await _movieRepository.CreateAsync(movie);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _movieRepository.DeleteByIdAsync(id);
        }

        public Task<bool> ExistsByIdAsync(Guid id)
        {
            return _movieRepository.ExistsByIdAsync(id);    
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
           return await _movieRepository.GetByIdAsync(id);
        }

        public async Task<Movie?> GetBySlugAsync(string slug)
        {
           return await _movieRepository.GetBySlugAsync(slug);
        }

        public async Task<Movie?> UpdateAsync(Movie movie)
        {
            await _movieValidator.ValidateAndThrowAsync(movie);
            var movieExist = await _movieRepository.ExistsByIdAsync(movie.Id);
            if (!movieExist)
            {
                return null;
            }

            await _movieRepository.UpdateAsync(movie);
            return movie;
        }
    }
}
