using Cinebook.Resources;
using FluentValidation;

namespace Cinebook.Infrastructure.Errors;

public static class ExceptionMapper
{
    public static ApplicationError Map(Exception ex)
    {
        return ex switch
        {
            NotFoundException => new NotFoundError(ApplicationErrors.NotFoundError_Title, ex.Message),
            AlreadyExistsException => new AlreadyExistsError(ApplicationErrors.AlreadyExistsError_Title, ex.Message),
            ValidationException validationException => new ValidationError(ApplicationErrors.ValidationError_Title,
                ApplicationErrors.ValidationError_Message, validationException.Errors),
            InternalServerException => new InternalServerError(ApplicationErrors.InternalServerError_Title, ex.Message),
            LogicErrorException => new InternalServerError(ApplicationErrors.InternalServerError_Title, ex.Message),
            _ => new InternalServerError(ApplicationErrors.InternalServerError_Title, ex.Message)
        };
    }
}