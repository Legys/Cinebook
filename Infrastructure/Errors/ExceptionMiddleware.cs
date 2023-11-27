using System.Text.Json;

namespace Cinebook.Infrastructure.Errors;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static int GetStatusCode(ApplicationError error)
    {
        return error.Match(
            notFoundError => 404,
            alreadyExistsError => 409,
            validationError => 400,
            internalServerError => 500
        );
    }

    private static string GetTitle(ApplicationError error)
    {
        return error.Match<string>(
            notFoundError => notFoundError.Title,
            alreadyExistsError => alreadyExistsError.Title,
            validationError => validationError.Title,
            internalServerError => internalServerError.Title
        );
    }

    private static string GetMessage(ApplicationError error)
    {
        return error.Match(
            notFoundError => notFoundError.Message,
            alreadyExistsError => alreadyExistsError.Message,
            validationError => validationError.Message,
            internalServerError => internalServerError.Message
        );
    }

    private static IEnumerable<string> GetErrors(ApplicationError error)
    {
        return error.Match(
            notFoundError => new List<string>(),
            alreadyExistsE => new List<string>(),
            validationError => validationError.Errors.Select(e => e.ErrorMessage),
            internalServerError => new List<string>()
        );
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var error = ExceptionMapper.Map(exception);
        var statusCode = GetStatusCode(error);
        var response = new
        {
            title = GetTitle(error),
            status = statusCode,
            description = GetMessage(error),
            errors = GetErrors(error)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}