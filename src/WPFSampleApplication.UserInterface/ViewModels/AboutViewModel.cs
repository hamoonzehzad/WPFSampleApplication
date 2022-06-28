using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Stores;

namespace WPFSampleApplication.UserInterface.ViewModels;

public sealed class AboutViewModel : ViewModelBase
{
    #region Constructors

    public AboutViewModel(INavigationStore navigationStore) : base(navigationStore)
    {
        Title = "About";
        Subtitle = "a brief technical description";
        Body =
            $"This is a Windows Presentation Foundation (WPF) sample application written in .NET 6 using the MVVM architecture." +
            $"There are 2 projects in the solution. Service for communicating with external resources like apis and UserInterface fro having the viewmodels, views and resource dictionaries." +
            $"For having a json configuration file, I added a appsetting.json and configured it by using an IConfiguration (Microsoft.Extensions.Configuration) object in the app.xaml.cs. There is 2 type of configuration in appsettings.json.One for HttpClient settings and another for resiliency." +
            $"For having resiliency in http calls in service project I used Polly (Microsoft.Extensions.Http.Polly) and provided three diffrent strategies for resiliency immediate, linear and exponential." +
            $"Each project have it's own custom exception classes and error messages provided by resx files. There is a global exception handling event in the app.xaml.cs for DispatcherUnhandledException and depending on which type of exception is raised the application will handle it.";
    }

    #endregion

    #region Binding Properties

    /// <summary>
    /// Stores the body of the about page.
    /// </summary>
    public string? Body {
        get => _body;
        set {
            _body = value;
            OnPropertyChanged(nameof(Body));
        }
    }
    private string? _body;

    #endregion
}

