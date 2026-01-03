using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;

namespace Movies.Api.EndPoints.Movies
{
    public static class GetAllMovies
    {
        public const string Name = "GetAllMovies";

        public static IEndpointRouteBuilder MapGetAllMovies(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Movies.GetAll, async (
                [AsParameters]GetAllMoviesRequest request,HttpContext contex, 
                IMovieService movieService,
                CancellationToken cancellationToken) =>
            {
                var userId = contex.GetUserId();
                var options = request.MapToOptions().WithUser(userId);
                var movies = await movieService.GetAllAsync(options, cancellationToken);
                var movieCount = await movieService.GetCountAsync(options.Title, options.YearOfRelease, cancellationToken);

                var response = movies.MapToMoviesResponse(
                    request.Page.GetValueOrDefault(PagedRequest.DefaultPage), 
                    request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize),
                    movieCount);
                return TypedResults.Ok(response);
            })
            .WithName(Name);
            return app;
        }
    }
}
