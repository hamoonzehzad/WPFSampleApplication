using WPFSampleApplication.Service.Abstractions;

namespace WPFSampleApplication.Service.Exceptions;

public sealed class ServiceException : ExceptionBase
{
    public ServiceException(string? message) : base(message) { }
}
