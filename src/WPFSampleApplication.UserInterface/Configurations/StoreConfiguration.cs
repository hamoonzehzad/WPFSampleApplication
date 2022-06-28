using Microsoft.Extensions.DependencyInjection;
using WPFSampleApplication.UserInterface.Stores;

namespace WPFSampleApplication.UserInterface.Configurations;

/// <summary>
/// Configures all the stores in the application.
/// </summary>
public static class StoreConfiguration
{
    /// <summary>
    /// Adds all the stores in the application.
    /// </summary>
    /// <param name="serviceCollection">Specifies the contract for a collection of service descriptors.</param>
    public static void AddStores(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<INavigationStore, NavigationStore>();
    }
}
