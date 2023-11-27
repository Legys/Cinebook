namespace Cinebook.Application.Features.Movies.Model.Request;

public record UpdateMovieRequest(
    string Title,
    string Description,
    List<Guid> Genres,
    int DurationInMinutes,
    DateTime ReleaseDate
);