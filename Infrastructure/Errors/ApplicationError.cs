using FluentValidation.Results;
using OneOf;

namespace Cinebook.Infrastructure.Errors;

public class Error(string title, string message)
{
    public string Title { get; } = title;
    public string Message { get; } = message;
}

public class NotFoundError(string title, string message) : Error(title, message);

public class AlreadyExistsError(string title, string message) : Error(title, message);

public class ValidationError(string title, string message, IEnumerable<ValidationFailure> errors)
    : Error(title, message)
{
    public IEnumerable<ValidationFailure> Errors { get; } = errors;
}

public class InternalServerError(string title, string message) : Error(title, message);

[GenerateOneOf]
public partial class
    ApplicationError : OneOfBase<NotFoundError, AlreadyExistsError, ValidationError, InternalServerError>;