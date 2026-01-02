
using Movies.Contracts.Requests;
using Movies.Contracts.Response;
using Refit;

namespace Movies.Api.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IMoviesApi
    {
        [Get(ApiEndpoints.Movies.Get)]
        Task<MovieResponse> GetMovieAsync(string idOrSlug);

        [Get(ApiEndpoints.Movies.GetAll)]
        Task<MoviesResponse> GetMoviesAsync(GetAllMoviesRequest request);

        [Post(ApiEndpoints.Movies.Create)]
        Task<MovieResponse> CreateMovieAsync(CreateMovieRequest request);

        [Put(ApiEndpoints.Movies.Update)]
        Task<MovieResponse> UpdateMovieAsync(Guid id, UpdateMovieRequest request);

    }
}
