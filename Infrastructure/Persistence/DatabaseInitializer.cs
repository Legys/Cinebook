using Cinebook.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static void InitializeDatabase(this IServiceCollection services, EnvSettings envSettings)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(envSettings.DefaultConnection));
    }
}