using Cinebook.Application.Features.MovieSessions;
using Cinebook.Application.Features.Seats;
using Cinebook.Domain.Entities;
using Cinebook.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Infrastructure.Persistence.Seeder;

public class DatabaseSeeder(AppDbContext context, MovieSessionsFactory movieSessionsFactory, SeatsFactory seatsFactory)
{
    private async Task SeedData()
    {
        if (!context.Cinemas.Any())
        {
            var todayUtc = DateTime.Today.ToUniversalTime();
            var defaultOpeningAtUtc = new DateTime(todayUtc.Year, todayUtc.Month, todayUtc.Day).AddHours(8);
            var defaultClosingAtUtc = defaultOpeningAtUtc.AddHours(10);
            var cinemas = new List<Cinema>
            {
                new()
                {
                    Name = "Cineplex",
                    Address = "Republic Mall",
                    BaseTicketPrice = 1000,
                    OpeningAtUtc = defaultOpeningAtUtc,
                    ClosingAtUtc = defaultClosingAtUtc,
                    MovieSessions = [],
                    Seats = []
                },
                new()
                {
                    Name = "Cosmos",
                    Address = "Cosmos Mall",
                    BaseTicketPrice = 1000,
                    OpeningAtUtc = defaultOpeningAtUtc,
                    ClosingAtUtc = defaultClosingAtUtc,
                    MovieSessions = [],
                    Seats = []
                },
                new()
                {
                    Name = "Luxscreen",
                    Address = "Europa Business Park",
                    BaseTicketPrice = 1700,
                    OpeningAtUtc = defaultOpeningAtUtc,
                    ClosingAtUtc = defaultClosingAtUtc,
                    MovieSessions = [],
                    Seats = []
                }
            };
            var seatsConfig = new List<SeatConfig>
            {
                new(0, 8, SeatTypeEnum.Front),
                new(1, 8, SeatTypeEnum.Front),
                new(2, 10, SeatTypeEnum.Regular),
                new(3, 10, SeatTypeEnum.Regular),
                new(4, 10, SeatTypeEnum.Regular),
                new(5, 10, SeatTypeEnum.Regular),
                new(6, 10, SeatTypeEnum.Regular),
                new(7, 10, SeatTypeEnum.Regular),
                new(8, 10, SeatTypeEnum.Regular),
                new(9, 10, SeatTypeEnum.Regular),
                new(10, 10, SeatTypeEnum.Regular),
                new(11, 6, SeatTypeEnum.Lux)
            };

            cinemas.ForEach(cinema => cinema.Seats = seatsFactory.Init(cinema).CreateSeats(seatsConfig));

            var genres = new List<Genre>
            {
                new() { Name = "Action" },
                new() { Name = "Comedy" },
                new() { Name = "Drama" },
                new() { Name = "Horror" },
                new() { Name = "Mystery" },
                new() { Name = "Romance" },
                new() { Name = "Thriller" },
                new() { Name = "Science Fiction" },
                new() { Name = "Fantasy" },
                new() { Name = "Documentary" }
            };

            var movies = new List<Movie>
            {
                new()
                {
                    Title = "High impact",
                    Description =
                        "In the near future, \"High Impact\" unfolds in a world where humanity has harnessed advanced gravitational technology. The story follows Ava, a brilliant but rebellious gravity engineer, who discovers a plot to weaponize her latest invention. When her invention falls into the wrong hands, it threatens to disrupt the Earth's gravitational field, leading to catastrophic consequences.\n\nForced to team up with a group of renegade pilots known as the SkyRiders, Ava embarks on a high-stakes mission to retrieve her invention. The team navigates through a labyrinth of aerial battles, zero-gravity combat, and high-speed chases in futuristic cityscapes. As they get closer to the antagonist, a mysterious figure with a personal vendetta against Ava, they must use all their skills and Ava's technology to prevent global disaster.\n\n\"High Impact\" is a thrilling blend of action-packed sequences and cutting-edge science fiction, exploring themes of technology misuse, environmental peril, and the resilience of the human spirit. Ava's journey is not just about saving the world; it's about rediscovering her own power and the true potential of human innovation.",
                    ReleaseDateUtc = new DateTime(2017, 8, 5).ToUniversalTime(),
                    DurationInMinutes = 125,
                    Genres = [genres[0], genres[7]]
                },
                new()
                {
                    Title = "Mirror",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2020, 5, 17).ToUniversalTime(),
                    DurationInMinutes = 93,
                    Genres = [genres[1], genres[2]]
                },
                new()
                {
                    Title = "Paradise meadows",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2019, 12, 25).ToUniversalTime(),
                    DurationInMinutes = 180,
                    Genres = [genres[2], genres[5]]
                },
                new()
                {
                    Title = "The Last of Us",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2018, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 115,
                    Genres = [genres[3], genres[4]]
                },
                new()
                {
                    Title = "The Last of Us 2",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2019, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 123,
                    Genres = [genres[3], genres[4]]
                },
                new()
                {
                    Title = "Sinner",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2020, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 159,
                    Genres = [genres[1]]
                },
                new()
                {
                    Title = "The Awakening",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2021, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 122,
                    Genres = [genres[4]]
                },
                new()
                {
                    Title = "The Awakening 2",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2022, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 134,
                    Genres = [genres[4]]
                },
                new()
                {
                    Title = "The King",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2023, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 142,
                    Genres = [genres[8]]
                },
                new()
                {
                    Title = "Extinction",
                    Description = "",
                    ReleaseDateUtc = new DateTime(2024, 1, 1).ToUniversalTime(),
                    DurationInMinutes = 119,
                    Genres = [genres[5]]
                }
            };

            cinemas.ForEach(cinema => cinema.MovieSessions = movieSessionsFactory.Init(movies, cinema).CreateMovieSessions(DateTime.Today, 7));

            await context.Genres.AddRangeAsync(genres);
            await context.Movies.AddRangeAsync(movies);
            await context.Cinemas.AddRangeAsync(cinemas);
            await context.SaveChangesAsync();
        }
    }

    public async Task SeedAll()
    {
        await context.Database.MigrateAsync();
        await SeedData();
    }
}