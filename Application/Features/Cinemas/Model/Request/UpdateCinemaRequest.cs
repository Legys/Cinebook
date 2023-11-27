namespace Cinebook.Application.Features.Cinemas.Model.Request;

public record UpdateCinemaRequest(string Name, string Address, int BaseTicketPrice);