namespace Cinebook.Application.Features.Movies.Model.Request;

public record CreateMovieRequest(
    string Title,
    string Description,
    List<Guid> Genres,
    int DurationInMinutes,
    DateTime ReleaseDateUtc
);