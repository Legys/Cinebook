using Cinebook.Domain.Entities;
using Cinebook.Domain.Rules;

namespace Cinebook.Application.Features.MovieSessions;

public class MovieSessionsFactory()
{
    // Backing field for MoviesPool
    private List<Movie>? MoviesPoolInit;
    private List<Movie> MoviesPool
    {
        get
        {
            if (MoviesPoolInit is null)
                throw new InvalidOperationException("MovieSessionsFactory is not initialized. Use Init method to initialize it properly");

            return MoviesPoolInit;
        }
    }

    // Backing field for Cinema
    private Cinema? CinemaInit;
    private Cinema Cinema
    {
        get
        {
            if (CinemaInit is null)
                throw new InvalidOperationException("MovieSessionsFactory is not initialized. Use Init method to initialize it properly");

            return CinemaInit;
        }
    }
    public MovieSessionsFactory Init(List<Movie> moviesPool, Cinema cinema)
    {
        MoviesPoolInit = moviesPool;
        CinemaInit = cinema;
        return this;
    }
    public List<MovieSession> CreateMovieSessions(DateTime startDateUtc, int daysAhead)
    {
        var emptyListForDaysAhead = Enumerable.Repeat(new List<MovieSession>(), daysAhead);
        var movieSessionsAheadTest = emptyListForDaysAhead.SelectMany((dayMovieSessions, index) =>
        {
            var dayAhead = index;
            var nextDayTimeUtc = GetNextDayTimeUtc(startDateUtc, dayAhead);
            var nextDaySessions =
                ProduceDailyMovieSessions([], nextDayTimeUtc, MoviesPool, 0);
            dayMovieSessions.AddRange(nextDaySessions);
            return dayMovieSessions;
        });

        return movieSessionsAheadTest.ToList();
    }

    private DateTime GetNextDayTimeUtc(DateTime startDateUtc, int dayAhead)
    {
        return new DateTime(startDateUtc.Year, startDateUtc.Month, startDateUtc.Day + dayAhead).AddHours(Cinema.OpeningAtUtc.Hour);
    }

    private List<MovieSession> ProduceDailyMovieSessions(List<MovieSession> currentList,
        DateTime nextSessionStartTimeUtc, List<Movie> movies, int movieIndex)
    {
        var currentMovie = movies[movieIndex];

        if ((nextSessionStartTimeUtc.Hour >= Cinema.ClosingAtUtc.Hour) || (nextSessionStartTimeUtc.Hour < Cinema.OpeningAtUtc.Hour))
            return currentList;

        var nextSessionEndTimeUtc = nextSessionStartTimeUtc.AddMinutes(currentMovie.DurationInMinutes);

        var nextMovieSession = new MovieSession
        {
            Movie = currentMovie,
            Cinema = Cinema,
            SeatReservations = [],
            StartTimeUtc = nextSessionStartTimeUtc,
            EndTimeUtc = nextSessionEndTimeUtc
        };

        currentList.Add(nextMovieSession);
        var nextIndex = movieIndex + 1 >= movies.Count ? 0 : movieIndex + 1;
        var nextSessionStartTimeWithBreak = nextSessionEndTimeUtc.AddMinutes(CinemaConsts.SessionServiceDurationInMinutes);
        return ProduceDailyMovieSessions(currentList, nextSessionStartTimeWithBreak, movies, nextIndex);
    }
}