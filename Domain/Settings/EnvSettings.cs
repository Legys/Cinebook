namespace Cinebook.Domain.Settings;

public record EnvSettings
{
    public required string AllowedHosts { get; init; }
    public required string DefaultConnection { get; init; }
}