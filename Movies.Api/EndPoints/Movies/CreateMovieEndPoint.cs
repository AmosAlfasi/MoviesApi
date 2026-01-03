using Microsoft.AspNetCore.OutputCaching;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using System.Threading;

namespace Movies.Api.EndPoints.Movies
{
    public static class CreateMovieEndPoint
    {
        public const string Name = "CreateMovie";

        public static IEndpointRouteBuilder MapCreateMovie(this IEndpointRouteBuilder app)
        {
            app.MapPost(ApiEndpoints.Movies.Create, async (
                CreateMovieRequest request, IMovieService movieService,
                IOutputCacheStore outputCacheStore,
                CancellationToken token) =>
            {
                var movie = request.MapToMovie();
                await movieService.CreateAsync(movie, token);
                await outputCacheStore.EvictByTagAsync("movies", token);
                var response = movie.MapToMovieResponse();
                return TypedResults.CreatedAtRoute(response, GetMovieEndPoint.Name, new { idOrSlug = movie.Id });            
            })
            .WithName(Name)
            .RequireAuthorization(AuthConstants.TrustMemberPolicyName);
            return app;
        }
    }
}
