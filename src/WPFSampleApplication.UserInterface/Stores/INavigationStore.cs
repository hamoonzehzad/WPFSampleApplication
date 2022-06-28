using WPFSampleApplication.UserInterface.Abstractions;

namespace WPFSampleApplication.UserInterface.Stores;

/// <summary>
/// Controls and changes the state of the application.
/// </summary>
public interface INavigationStore
{
    /// <summary>
    /// Stores the current viewmodel as the state of application 
    /// </summary>
    IViewModelBase? CurrentViewModel { get; }

    /// <summary>
    /// Navigates the state to a view model via generic type.
    /// </summary>
    void NavigateTo<TViewModel>() where TViewModel : IViewModelBase;

    /// <summary>
    /// Navigates the state to the startup page.
    /// </summary>
    void NavigateToStartupView();

    /// <summary>
    /// Triggers when current viewmodel changes.
    /// </summary>
    event Action? CurrentViewModelChanged;
}