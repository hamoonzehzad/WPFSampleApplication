using WPFSampleApplication.Service.Exceptions;
using WPFSampleApplication.Service.Services;
using WPFSampleApplication.UserInterface.Abstractions;
using WPFSampleApplication.UserInterface.Exceptions;
using WPFSampleApplication.UserInterface.Resources;
using WPFSampleApplication.UserInterface.Stores;
using Prism.Commands;
using System.Collections.ObjectModel;

namespace WPFSampleApplication.UserInterface.ViewModels;

public sealed class PostViewModel : ViewModelBase
{
    #region Fields

    private readonly IPostService _postService;
    private ButtonContentState _postDataState;
    private List<int> _postIdList;
    private List<int> _postUserIdList;

    #endregion

    #region Enums

    private enum ButtonContentState
    {
        Id,
        UserId
    }

    #endregion

    #region Constructors

    public PostViewModel(INavigationStore navigationStore, IPostService postService) : base(navigationStore)
    {
        _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        _postDataState = ButtonContentState.Id;
        _postIdList = new List<int>();
        _postUserIdList = new List<int>();

        Title = "{JSON} PlaceHolder Posts";
        PostItemButtonCommand = new DelegateCommand(PostItemButton_Clicked);

        Task.Run(GetButtonContentsAsync).Wait();
    }

    #endregion

    #region Binding Properties

    /// <summary>
    /// Stores the content of the post buttons.
    /// </summary>
    public ObservableCollection<int>? ButtonContents {
        get => _buttonContents;
        set {
            _buttonContents = value;
            OnPropertyChanged(nameof(ButtonContents));
        }
    }
    private ObservableCollection<int>? _buttonContents;

    #endregion

    #region Commands

    public DelegateCommand PostItemButtonCommand { get; }

    #endregion

    #region Operations

    /// <summary>
    /// Gets and arranges the post data.
    /// </summary>
    private async Task GetButtonContentsAsync()
    {
        // Gets all the posts from post service class. 
        var postIndexDtoList = await _postService.GetAllPostsAsync();

        // Selects the ids from the post list.
        _postIdList = postIndexDtoList
            .Select(model => model.Id)
            .ToList();

        // Selects the userids from the post list.
        _postUserIdList = postIndexDtoList
            .Select(model => model.UserId)
            .ToList();

        ShowData();
    }

    /// <summary>
    /// Changes the state of post button contents.
    /// </summary>
    private void ChangeButtonContentState()
    {
        _postDataState = _postDataState is ButtonContentState.Id
            ? ButtonContentState.UserId
            : ButtonContentState.Id;
    }

    /// <summary>
    /// Changes the button content by changing the binding property based on the button content state.
    /// </summary>
    private void ShowData()
    {
        ButtonContents = _postDataState is ButtonContentState.Id
            ? new ObservableCollection<int>(_postIdList)
            : new ObservableCollection<int>(_postUserIdList);

        // Shows to the user what is the current state.
        Subtitle = $"Currently the state is: {_postDataState}";
    }

    #endregion

    #region Events

    private void PostItemButton_Clicked()
    {
        ChangeButtonContentState();
        ShowData();
    }

    #endregion
}

