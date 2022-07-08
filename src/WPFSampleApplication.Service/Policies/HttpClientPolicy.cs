using Microsoft.Extensions.Options;
using WPFSampleApplication.Service.Abstractions;
using WPFSampleApplication.Service.Configurations.Policy;
using Polly;
using Polly.Retry;

namespace WPFSampleApplication.Service.Policies;

/// <summary>
/// This class manages the policies of http client calls.
/// It's very important to have resiliency when it comes to communicate with external services.
/// </summary>
internal sealed class HttpClientPolicy : ServicePolicyBase, IHttpClientPolicy
{
    #region Fields

    private readonly PolicyOptions _policyOptions;

    #endregion

    #region  Constructors

    public HttpClientPolicy(IOptions<PolicyOptions> policyOptions)
    {
        _policyOptions = policyOptions?.Value ?? throw new ArgumentNullException(nameof(policyOptions));

        ImmediateRetryPolicy = Policy
            .HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
            .RetryAsync(_policyOptions.HttpClientRetry);

        LinearRetryPolicy = Policy
            .HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
            .WaitAndRetryAsync(_policyOptions.HttpClientRetry, retryAttempt => _policyOptions.HttpClientDelay);

        ExponentialRetryPolicy = Policy
           .HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
           .WaitAndRetryAsync(_policyOptions.HttpClientRetry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(_policyOptions.HttpClientPower, retryAttempt)));
    }

    #endregion

    #region Properties

    /// <summary>
    /// If the http request call fails, it will retry immediatly for HttpClientRetry times.
    /// </summary>
    public AsyncRetryPolicy<HttpResponseMessage> ImmediateRetryPolicy { get; }

    /// <summary>
    /// If the http request call fails, it will wait for HttpClientDelay and then retries for HttpClientRetry times.
    /// </summary>
    public AsyncRetryPolicy<HttpResponseMessage> LinearRetryPolicy { get; }

    /// <summary>
    /// If the http request call fails, it will wait for retry attempt count power by HttpClientPower and then retries for HttpClientRetry times.
    /// </summary>
    public AsyncRetryPolicy<HttpResponseMessage> ExponentialRetryPolicy { get; }
    
    #endregion
}
