namespace WPFSampleApplication.Service.Abstractions;

/// <summary>
/// Base class of all exceptions in the service project.
/// </summary>
public abstract class ExceptionBase : Exception
{
    public ExceptionBase(string? message) : base(message) { }
}
