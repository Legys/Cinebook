using Cinebook.Application.Behaviors;
using MediatR;

namespace Cinebook.Application;

public static class MediatorInitializer
{
    public static void InitializeMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}