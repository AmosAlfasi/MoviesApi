using Microsoft.AspNetCore.OutputCaching;
using Movies.Api.Auth;
using Movies.Application.Services;
using System.Threading;

namespace Movies.Api.EndPoints.Movies
{
    public static class DeleteMovieEndPoint
    {
        public const string Name = "DeleteMovie";

        public static IEndpointRouteBuilder MapDeleteMovie(this IEndpointRouteBuilder app)
        {
            app.MapDelete(ApiEndpoints.Movies.Delete, async (
                Guid id, IOutputCacheStore outputCacheStore, IMovieService movieService,
                HttpContext context, CancellationToken token) =>
            {
                var deleted = await movieService.DeleteByIdAsync(id, token);
                if (!deleted)
                {
                    return Results.NotFound();
                }
                await outputCacheStore.EvictByTagAsync("movies", token);
                return Results.Ok();
            })
            .WithName(Name)
            .RequireAuthorization(AuthConstants.AdminUserPolicyName);
            return app;
        }
    }
}
