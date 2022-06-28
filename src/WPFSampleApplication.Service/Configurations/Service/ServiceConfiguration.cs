using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPFSampleApplication.Service.Services;

namespace WPFSampleApplication.Service.Configurations.Service;

/// <summary>
/// Configures all the service classes in the service collection.
/// </summary>
public static class ServiceConfiguration
{
    /// <summary>
    /// Adds all the service classes in the service collection.
    /// </summary>
    /// <param name="serviceCollection">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    public static void AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Gets the service settings from the appsettings
        var serviceConfigurationSection = configuration.GetRequiredSection("service");

        serviceCollection.Configure<ServiceOptions>(serviceConfigurationSection);
        serviceCollection.AddSingleton<IPostService, PostService>();
    }
}
