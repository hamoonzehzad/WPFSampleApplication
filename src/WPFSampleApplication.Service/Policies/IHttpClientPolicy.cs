using Polly.Retry;

namespace WPFSampleApplication.Service.Policies;
internal interface IHttpClientPolicy
{
    /// <summary>
    /// If the http request call fails, it will retry immediatly for HttpClientRetry times.
    /// </summary>
    AsyncRetryPolicy<HttpResponseMessage> ImmediateRetryPolicy { get; }

    /// <summary>
    /// If the http request call fails, it will wait for HttpClientDelay and then retries for HttpClientRetry times.
    /// </summary>
    AsyncRetryPolicy<HttpResponseMessage> LinearRetryPolicy { get; }

    /// <summary>
    /// If the http request call fails, it will wait for retry attempt count power by HttpClientPower and then retries for HttpClientRetry times.
    /// </summary>
    AsyncRetryPolicy<HttpResponseMessage> ExponentialRetryPolicy { get; }
}