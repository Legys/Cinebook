using Cinebook.Application.Features.Movies.Command;
using FluentValidation;

namespace Cinebook.Application.Features.Movies.Validator;

public class CreateMovieRequestValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieRequestValidator()
    {
        RuleFor(m => m.CreateMovieRequest.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");

        RuleFor(m => m.CreateMovieRequest.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(m => m.CreateMovieRequest.Genres)
            .NotEmpty().WithMessage("Genres are required.");

        RuleFor(m => m.CreateMovieRequest.DurationInMinutes)
            .NotEmpty().WithMessage("Duration is required.")
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(m => m.CreateMovieRequest.ReleaseDateUtc)
            .NotEmpty().WithMessage("Release date is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Release date must be in the past.");
    }
}
