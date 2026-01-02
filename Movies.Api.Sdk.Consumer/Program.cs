using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Sdk;
using Movies.Api.Sdk.Consumer;
using Movies.Contracts.Requests;
using Refit;
using System.Text.Json;

//var moviesApi = RestService.For<IMoviesApi>("https://localhost:44324");


var services = new ServiceCollection();

services
    .AddHttpClient()
    .AddSingleton<AuthTokenProvider>()
    .AddRefitClient<IMoviesApi>(x=>new RefitSettings
    {
        AuthorizationHeaderValueGetter = async (msg, ct) => 
        await x.GetRequiredService<AuthTokenProvider>().GetTokenAsync()
    })
    .ConfigureHttpClient(x=>
    x.BaseAddress = new Uri("https://localhost:44324"));

var provider = services.BuildServiceProvider();


var moviesApi = provider.GetRequiredService<IMoviesApi>();


var movie = await moviesApi.GetMovieAsync("29cbc4cb-cdca-4d3f-90ee-1feb82f31423");

var newMovie = await moviesApi.CreateMovieAsync(new CreateMovieRequest
{
    Title = "Lion King",
    YearOfRelease = 1994,
    Genres = new[] { "Drama" }
});
var t = newMovie.Id;
var req = new GetAllMoviesRequest
{
    Page = 1,
    PageSize = 3,
    SortBy = null,
    Title = null,
    Year = null
};
var movies = await moviesApi.GetMoviesAsync(req);

Console.WriteLine(JsonSerializer.Serialize(movie));