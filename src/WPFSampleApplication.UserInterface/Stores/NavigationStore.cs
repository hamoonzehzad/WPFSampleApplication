using Microsoft.Extensions.DependencyInjection;
using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.ViewModels;

namespace WPFSampleApplication.UserInterface.Stores;

/// <summary>
/// Controls and changes the state of the application.
/// </summary>
public sealed class NavigationStore : StoreBase, INavigationStore
{
    #region Fields

    /// <summary>
    /// For injecting the viewmodels
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    #endregion

    #region Constructors
    public NavigationStore(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    #endregion

    #region Binding Properties

    /// <summary>
    /// Stores the current viewmodel as the state of application 
    /// </summary>
    public IViewModelBase? CurrentViewModel 
    {
        get => _currentViewModel;
        private set 
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }
    private IViewModelBase? _currentViewModel;

    #endregion

    #region Events
    
    /// <summary>
    /// Triggers when current viewmodel changes.
    /// </summary>
    public event Action? CurrentViewModelChanged;
    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    #endregion

    #region Operations

    /// <summary>
    /// Navigates the state to a view model via generic type.
    /// </summary>
    public void NavigateTo<TViewModel>() where TViewModel : IViewModelBase
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
    }

    /// <summary>
    /// Navigates the state to the startup page.
    /// </summary>
    public void NavigateToStartupView()
    {
        NavigateTo<HomeViewModel>();
    }

    #endregion
}
