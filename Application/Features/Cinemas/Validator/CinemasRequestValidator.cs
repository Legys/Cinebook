using Cinebook.Application.Features.Cinemas.Command;
using FluentValidation;

namespace Cinebook.Application.Features.Cinemas.Validator;

public class CinemasRequestValidator : AbstractValidator<CreateCinemaCommand>
{
    public CinemasRequestValidator()
    {
        RuleFor(m => m.Request.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(m => m.Request.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");

        RuleFor(m => m.Request.BaseTicketPrice)
            .NotEmpty().WithMessage("Base ticket price is required.")
            .GreaterThan(0).WithMessage("Base ticket price must be greater than 0.");

        RuleFor(m => m.Request.OpeningAtUtc)
            .NotEmpty().WithMessage("Opening date is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Opening date must be in the past.");

        RuleFor(m => m.Request.ClosingAtUtc)
            .NotEmpty().WithMessage("Closing date is required.")
            .GreaterThan(m => m.Request.OpeningAtUtc).WithMessage("Closing date must be greater than opening date.");
    }
}