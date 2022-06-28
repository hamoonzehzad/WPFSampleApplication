using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Stores;

namespace WPFSampleApplication.UserInterface.ViewModels;

public sealed class HomeViewModel : ViewModelBase
{
    #region Constructors
    
    public HomeViewModel(INavigationStore navigationStore) : base(navigationStore)
    {
        Title = "Home";
        Subtitle = "WPF Sample Application";
        Body = 
            $"Welcome to the WPF Sample Application" +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"This application will fetch the 100 posts from JSON PlaceHolder and show the id and userid fields in 10x10 square map." +
            $"To see how it works click on the posts button in the navigation menu." +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"You can find a brief technical description about this application in the about section." +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"Enjoy...";
    }

    #endregion

    #region Binding Properties

     /// <summary>
    /// Stores the body of the home page. 
    /// </summary>
    public string? Body 
    { 
        get => _body; 
        set 
        { 
            _body = value;
            OnPropertyChanged(nameof(Body));
        } 
    }
    private string? _body; 

    #endregion
}

