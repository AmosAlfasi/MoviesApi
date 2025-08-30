using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class MovieMockRepository : IMovieRepository
    {
        private readonly List<Movie> _movies = new List<Movie>();
        public Task<bool> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            _movies.Add(movie);
            return Task.FromResult(true);
        }
        public Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var movie = _movies.SingleOrDefault(m => m.Id == id);
            return Task.FromResult(movie);
        }
        public Task<IEnumerable<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_movies.AsEnumerable());
        }

        public Task<bool> UpdateAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            var movieIndex = _movies.FindIndex(x => x.Id == movie.Id);
            if (movieIndex == -1)
            {
                return Task.FromResult(false);
            }
            _movies[movieIndex] = movie;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var removedCount = _movies.RemoveAll(x => x.Id == id);
            return Task.FromResult(removedCount > 0);
        }
        public Task<Movie?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            var movie = _movies.SingleOrDefault(m => m.Slug == slug);
            return Task.FromResult(movie);
        }

        public Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
