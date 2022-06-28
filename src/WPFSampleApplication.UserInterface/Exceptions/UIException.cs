using WPFSampleApplication.UserInterface.Abstractions;
using System.Windows;

namespace WPFSampleApplication.UserInterface.Exceptions;

public sealed class UIException : ExceptionBase
{
    public UIException(string message, bool isFatal) : base(message)
    {
        IsFatal = isFatal;
    }

    /// <summary>
    /// Determines that should application shut down itself after this exception or not;
    /// </summary>
    public bool IsFatal { get; set; }
}
