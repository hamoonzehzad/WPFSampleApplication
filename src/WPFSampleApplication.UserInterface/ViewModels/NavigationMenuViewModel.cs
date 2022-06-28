using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Stores;
using Prism.Commands;
using System.Windows;

namespace WPFSampleApplication.UserInterface.ViewModels;

public sealed class NavigationMenuViewModel : ViewModelBase
{
    #region Constructors

    public NavigationMenuViewModel(INavigationStore navigationStore) : base(navigationStore)
    {
        HomeButtonCommand = new DelegateCommand(HomeButton_Clicked);
        AboutButtonCommand = new DelegateCommand(AboutButton_Clicked);
        PostButtonCommand = new DelegateCommand(PostButton_Clicked);
        QuitButtonCommand = new DelegateCommand(QuitButton_Clicked);
    }

    #endregion

    #region Commands

    public DelegateCommand HomeButtonCommand { get; }
    public DelegateCommand AboutButtonCommand { get; }
    public DelegateCommand PostButtonCommand { get; }
    public DelegateCommand QuitButtonCommand { get; }

    #endregion

    #region Events

    private void HomeButton_Clicked()
    {
        NavigationStore.NavigateTo<HomeViewModel>();
    }
    private void AboutButton_Clicked()
    {
        NavigationStore.NavigateTo<AboutViewModel>();
    }
    private void PostButton_Clicked()
    {
        NavigationStore.NavigateTo<PostViewModel>();
    }
    private void QuitButton_Clicked()
    {
        var messageBoxResult = MessageBox.Show("Are you sure you want to quit?", "QUIT", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (messageBoxResult == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
    }

    #endregion
}

