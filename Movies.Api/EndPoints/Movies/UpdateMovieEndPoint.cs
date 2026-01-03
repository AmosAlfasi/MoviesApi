using Microsoft.AspNetCore.OutputCaching;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using System.Threading;

namespace Movies.Api.EndPoints.Movies
{
    public static class UpdateMovieEndPoint
    {
        public const string Name = "UpdateMovie";

        public static IEndpointRouteBuilder MapUpdateMovie(this IEndpointRouteBuilder app)
        {
            app.MapPut(ApiEndpoints.Movies.Create, async (
                Guid id,
                HttpContext context,
                UpdateMovieRequest request, IMovieService movieService,
                IOutputCacheStore outputCacheStore,
                CancellationToken token) =>
            {
                var userId = context.GetUserId();
                var movie = request.MapToMovie(id);
                var updatedMovie = await movieService.UpdateAsync(movie, userId, token);
                if (updatedMovie is null)
                {
                    return Results.NotFound();
                }
                await outputCacheStore.EvictByTagAsync("movies", token);
                var response = updatedMovie.MapToMovieResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .RequireAuthorization(AuthConstants.TrustMemberPolicyName); 
            return app;
        }
    }
}
