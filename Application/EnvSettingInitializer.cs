using Cinebook.Domain.Settings;
using Cinebook.Resources;

namespace Cinebook.Application;

public static class EnvSettingInitializer
{
    public static EnvSettings InitializeEnvSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var envSettings = configuration.GetSection("Env").Get<EnvSettings>();

        if (envSettings is null)
            throw new Exception(SystemMessages.ResourceManager.GetString("EnvSettingsInitializationFail"));

        services.AddTransient(_ => envSettings);

        return envSettings;
    }
}