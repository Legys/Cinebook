namespace Cinebook.Application.Features.Cinemas.Model.Request;

public record CreateCinemaRequest(string Name, string Address, int BaseTicketPrice, DateTime OpeningAtUtc, DateTime ClosingAtUtc);