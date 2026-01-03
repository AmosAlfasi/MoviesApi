using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using System.Threading;

namespace Movies.Api.EndPoints.Movies
{
    public static class GetMovieEndPoint
    {
        public const string Name = "GetMovie";

        public static IEndpointRouteBuilder MapGetMovie(this IEndpointRouteBuilder app)
        {
            app.MapGet(ApiEndpoints.Movies.Get, async (
                string idOrSlug, IMovieService movieService,
                HttpContext context, CancellationToken token) =>
            {
                var userId = context.GetUserId();
                var movie = Guid.TryParse(idOrSlug, out var id) ?
                    await movieService.GetByIdAsync(id, userId, token) :
                    await movieService.GetBySlugAsync(idOrSlug, userId, token);
                if (movie is null)
                {
                    return Results.NotFound();
                }
                var response = movie.MapToMovieResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name);
            return app;
        }
    }
}
