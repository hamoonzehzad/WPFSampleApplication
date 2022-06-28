using Microsoft.Extensions.DependencyInjection;
using WPFSampleApplication.UserInterface.Abstractions;

namespace WPFSampleApplication.UserInterface.Configurations;

/// <summary>
/// Configures all the viewmodels in the application.
/// </summary>
public static class ViewModelConfiguration
{
    /// <summary>
    /// Find and adds all the viewmodels in the application.
    /// </summary>
    /// <param name="serviceCollection">Specifies the contract for a collection of service descriptors.</param>
    public static void AddViewModels(this IServiceCollection serviceCollection)
    {
        // Adding all the view models via assembly define types using a sweet sweet lambda expression syntax.
        // Here we are adding viewmodels as singleton, this is because we want to keep the last state of every 
        // viewmodel when we navigate to another viewmodel.
        // Choosing transient lifecycle can cause the oposite behaviour which means that we have a fresh viewmodel
        // when ever we navigate to that viewmodel.
        // In real projects the life cycle of a viewmodel depends on the business of that application.
        typeof(IViewModelBase)
            .Assembly
            .DefinedTypes
            .Where(typeInfo
                => typeof(IViewModelBase).IsAssignableFrom(typeInfo)
                && typeInfo.IsSealed)
            .Select(viewModelTypeInfo => viewModelTypeInfo.AsType())
            .ToList()
            .ForEach(viewModelType => serviceCollection.AddSingleton(viewModelType));
    }
}
