namespace WPFSampleApplication.Service.Configurations.Policy;

public sealed class PolicyOptions
{
    /// <summary>
    /// How many times a http call should retry.
    /// </summary>
    public int HttpClientRetry { get; init; }

    /// <summary>
    /// How long should a failed http call waits to try again.
    /// </summary>
    public TimeSpan HttpClientDelay { get; init; }

    /// <summary>
    /// The power for exponential policy.
    /// </summary>
    public double HttpClientPower { get; init; }
}