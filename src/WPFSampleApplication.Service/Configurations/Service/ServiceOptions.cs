namespace WPFSampleApplication.Service.Configurations.Service;

public sealed class ServiceOptions
{
    /// <summary>
    /// The base address of JSON PlaceHolder api.
    /// </summary>
    public string RequestBaseAddress { get; init; } = string.Empty;

    /// <summary>
    /// Accept header for http call to JSON PlaceHolder api.
    /// </summary>
    public string RequestAcceptHeader { get; init; } = string.Empty;
    
    /// <summary>
    /// How long a http client call should wait fro response.
    /// </summary>
    public TimeSpan RequestTimeout { get; init; }
}