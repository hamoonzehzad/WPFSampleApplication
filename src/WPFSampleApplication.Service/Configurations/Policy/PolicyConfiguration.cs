using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPFSampleApplication.Service.Policies;

namespace WPFSampleApplication.Service.Configurations.Policy;

/// <summary>
/// Configures policies in the service collection.
/// </summary>
public static class PolicyConfiguration
{
    /// <summary>
    /// Adds all the policy classes in the service collection.
    /// </summary>
    /// <param name="serviceCollection">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    public static void AddPolicies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Gets the policy settings from the appsettings
        var serviceConfigurationSection = configuration.GetRequiredSection("policy");

        serviceCollection.Configure<PolicyOptions>(serviceConfigurationSection);
        serviceCollection.AddSingleton<IHttpClientPolicy, HttpClientPolicy>();
    }
}
