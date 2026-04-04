using Microsoft.Extensions.DependencyInjection;

namespace GeneralCore.Storage;

public static class StorageExtensions
{
    public static IServiceCollection AddMinioStorage(this IServiceCollection services, StorageConfig config)
    {
        services.AddSingleton(config);
        services.AddScoped<IStorageService, MinioStorageService>();
        return services;
    }
}
