namespace Movies.Api.EndPoints.Movies
{
    public static class MovieEndPointExtentions
    {
        public static IEndpointRouteBuilder MapMovieEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGetMovie();
            app.MapCreateMovie();
            app.MapGetAllMovies();
            app.MapUpdateMovie();
            app.MapDeleteMovie();
            return app;
        }
    }
}
