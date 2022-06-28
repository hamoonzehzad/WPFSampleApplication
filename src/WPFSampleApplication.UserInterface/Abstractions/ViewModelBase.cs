using WPFSampleApplication.UserInterface.Stores;
using System.ComponentModel;

namespace WPFSampleApplication.UserInterface.Abstractions;

/// <summary>
/// Base class of all viewmodel classes.
/// </summary>
public abstract class ViewModelBase : IViewModelBase
{
    #region Constructors
    
    public ViewModelBase(INavigationStore navigationStore)
    {
        // All viewmodel shoud inject the navigation in order  to have a better control on the application state.
        NavigationStore = navigationStore ?? throw new ArgumentNullException(nameof(navigationStore));
    }

    #endregion

    #region Properties

    /// <summary>
    /// Manages the navigation state for viewmodels
    /// </summary>
    protected INavigationStore NavigationStore { get; }

    #endregion

    #region Binding Properties
    
    /// <summary>
    /// Shows the title of views in layot of window.
    /// </summary>
    public virtual string? Title {
        get => _title;
        set {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    private string? _title;

    /// <summary>
    /// Shows the subtitle of views in layot of window.
    /// </summary>
    public virtual string? Subtitle {
        get => _subtitle;
        set {
            _subtitle = value;
            OnPropertyChanged(nameof(Subtitle));
        }
    }
    private string? _subtitle;

    #endregion

    #region Events

    /// <summary>
    /// This will notify the change of a property from INotifyPropertyChanged.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged 
    {
        add 
        {
            // Prevents from adding duplicate handler.
            if (_propertyChanged is null || !_propertyChanged.GetInvocationList().Contains(value))
            {
                _propertyChanged += value;
            }
        }
        remove 
        {
            _propertyChanged -= value;
        }
    }
    private PropertyChangedEventHandler? _propertyChanged;

    /// <summary>
    /// Notifies that the property with this name ihas been changed.
    /// </summary>
    /// <param name="propertyName"></param>
    protected void OnPropertyChanged(string propertyName)
    {
        _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    #endregion
}
