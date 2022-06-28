using System.ComponentModel;

namespace WPFSampleApplication.UserInterface.Abstractions;

public interface IViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Shows the title of views in layot of window.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Shows the subtitle of views in layot of window.
    /// </summary>
    string? Subtitle { get; set; }
}
