namespace Cinebook.Domain.Entities;

public class Movie
{
    public Guid MovieId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required DateTime ReleaseDateUtc { get; set; }
    public required int DurationInMinutes { get; set; }
    public List<Genre> Genres { get; set; } = [];

    public Movie Update(string title, string description, DateTime releaseDateUtc, int durationInMinutes,
        List<Genre> genres)
    {
        Title = title;
        Description = description;
        ReleaseDateUtc = releaseDateUtc;
        DurationInMinutes = durationInMinutes;
        Genres = genres;
        return this;
    }
}