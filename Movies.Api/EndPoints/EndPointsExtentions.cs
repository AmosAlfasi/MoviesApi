using Movies.Api.EndPoints.Movies;
using Movies.Api.EndPoints.Ratings;

namespace Movies.Api.EndPoints
{
    public static class EndPointsExtentions
    {
        public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapMovieEndPoint();
            app.MapRatingEndPoints();
            return app;
        }
    }
}
