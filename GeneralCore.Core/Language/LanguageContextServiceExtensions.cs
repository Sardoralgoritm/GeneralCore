using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralCore.Language;

public static class LanguageContextServiceExtensions
{
    public static IServiceCollection AddLanguageContext(
        this IServiceCollection services,
        Action<LanguageContextOptions>? configure = null)
    {
        services.AddHttpContextAccessor();

        var options = new LanguageContextOptions();
        configure?.Invoke(options);

        services.AddSingleton(options);
        services.AddScoped<ILanguageContext, HttpLanguageContext>();
        services.AddSingleton<IStartupFilter, LanguageContextStartupFilter>();

        return services;
    }
}
