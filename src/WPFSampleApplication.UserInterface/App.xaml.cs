using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WPFSampleApplication.Service.Configurations.Policy;
using WPFSampleApplication.Service.Configurations.Service;
using WPFSampleApplication.Service.Exceptions;
using WPFSampleApplication.UserInterface.Configurations;
using WPFSampleApplication.UserInterface.Exceptions;
using WPFSampleApplication.UserInterface.Stores;
using WPFSampleApplication.UserInterface.ViewModels;
using WPFSampleApplication.UserInterface.Views;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace WPFSampleApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Configuration that will use the appsettings.json
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Using for dependency injection
    /// </summary>
    private readonly IServiceProvider _serviceProvider;


    /// <summary>
    /// Controls and changes the state of the application.
    /// </summary>
    private readonly INavigationStore _navigationStore;

    /// <summary>
    /// Entry point of application.
    /// </summary>
    public App()
    {
        //Preparing the service provider for dependency injection
        _configuration = BuildConfigurations();
        _serviceProvider = BuildServiceProvider();

        // Getting navigation store as the state of the application.
        _navigationStore = _serviceProvider.GetRequiredService<INavigationStore>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        // Comment this method if you want to have better startup time.
        PreRenderViewModels();

        // Getting ready for starting the main window
        var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

        var mainWindow = new MainWindow
        {
            DataContext = mainWindowViewModel
        };

        // Setting the home page of application
        _navigationStore.NavigateToStartupView();

        //Show the main window 
        mainWindow.Show();

        base.OnStartup(e);
    }

    /// <summary>
    /// Activates all the viewmodels from service provider.
    /// Can achieve better navigation performance but slower startup time.
    /// This will work only on viewmodels with singleton lifecycle
    /// </summary>
    private void PreRenderViewModels()
    {
        // activating the viewmodels by getting them from service provider
        _serviceProvider.GetRequiredService<HomeViewModel>();
        _serviceProvider.GetRequiredService<AboutViewModel>();
        _serviceProvider.GetRequiredService<PostViewModel>();
    }


    /// <summary>
    /// Adds the appsettings.json file to configuration object 
    /// </summary>
    /// <returns>IConfiguration with application settings</returns>
    private static IConfiguration BuildConfigurations()
    {
        var configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        return configurationRoot;
    }

    /// <summary>
    /// Adds the configurations to the service collection and builds the service provider
    /// </summary>
    /// <returns>IServiceProvider from the service collection</returns>
    private IServiceProvider BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(_configuration);
        serviceCollection.AddHttpClient();
        serviceCollection.AddPolicies(_configuration);
        serviceCollection.AddServices(_configuration);
        serviceCollection.AddStores();
        serviceCollection.AddViewModels();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider;
    }

    /// <summary>
    /// Global Exception Handler
    /// </summary>
    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;

        if (e.Exception.InnerException is ServiceException serviceException)
        {
            MessageBox.Show(serviceException.Message, "Service Error", MessageBoxButton.OK, MessageBoxImage.Error);

            Current.Shutdown();

            // If you are using prerender viewmodels you cannot navigate to the home when exception happens.
            // _navigationStore.NavigateToStartupView();
        }
        else if (e.Exception.InnerException is UIException uiException)
        {
            MessageBox.Show(uiException.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);

            if (uiException.IsFatal)
            {
                Current.Shutdown();
            }
            else
            {
                // If you are using prerender viewmodels you cannot navigate to the home when exception happens.
                _navigationStore.NavigateToStartupView();
            }
        }
        else
        {
            var caption = e.Exception.InnerException is null 
                ? e.Exception.GetType().Name 
                : e.Exception.InnerException.GetType().Name;

            var errorMessage = e.Exception.InnerException?.Message ?? e.Exception.Message;

            MessageBox.Show(errorMessage, caption , MessageBoxButton.OK, MessageBoxImage.Error);

            Current.Shutdown();
        }
    }
}
