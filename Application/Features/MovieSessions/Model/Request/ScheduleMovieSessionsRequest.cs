namespace Cinebook.Application.Features.MovieSessions.Model.Request;

public record ScheduleMovieSessionsRequest(DateTime StartDate, int DaysAhead);
