using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Stores;
using System.ComponentModel;

namespace WPFSampleApplication.UserInterface.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    #region Constructors

    public MainWindowViewModel(INavigationStore navigationStore, NavigationMenuViewModel navigationMenuViewModel) : base(navigationStore)
    {
        // Navigation is fixed in layout so we inject it`s viewmodel manualy.
        NavigationMenuViewModel = navigationMenuViewModel ?? throw new ArgumentNullException(nameof(navigationMenuViewModel));

        // Subscribes on navigation store for being notified if the current viewmodel has been changed.
        NavigationStore.CurrentViewModelChanged += NavigationStore_CurrentViewModelChanged;
    }

    #endregion

    #region Binding Properties

    /// <summary>
    /// Binds the navigation menu view model on it`s view.
    /// </summary>
    public IViewModelBase? NavigationMenuViewModel { get; }
    
    /// <summary>
    /// Binds the current view model to the content control object.
    /// </summary>
    public IViewModelBase? CurrentViewModel => NavigationStore.CurrentViewModel;

    /// <summary>
    /// Binds the title from the current viewmodel on the title text block on the window layout.
    /// </summary>
    public override string? Title => NavigationStore.CurrentViewModel!.Title?.ToUpper();

    /// <summary>
    /// Binds the subtitle from the current viewmodel on the subtitle text block on the window layout.
    /// </summary>
    public override string? Subtitle => NavigationStore.CurrentViewModel!.Subtitle?.ToUpper();

    #endregion

    #region Events

    private void NavigationStore_CurrentViewModelChanged()
    {
        // Updates all the properties in the main window.
        OnPropertyChanged(string.Empty);

        // Subscribes for being notified if any of the current viewmodel properties have been changed.
        NavigationStore.CurrentViewModel!.PropertyChanged += CurrentViewModel_PropertyChanged;
    }
    private void CurrentViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // We will check if property changed is related to properties of main window or not.
        // If this is the case we reftesh the properties.
        if (e.PropertyName is nameof(Title))
        {
            OnPropertyChanged(nameof(Title));
        }
        else if (e.PropertyName is nameof(Subtitle))
        {
            OnPropertyChanged(nameof(Subtitle));
        }
    }

    #endregion
}
