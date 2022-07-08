using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WPFSampleApplication.Service.Configurations.Service;
using WPFSampleApplication.Service.Exceptions;
using WPFSampleApplication.Service.Policies;
using WPFSampleApplication.Service.Resources;
using Polly.Retry;
using System.Net.Http.Headers;
using System.Text;

namespace WPFSampleApplication.Service.Abstractions;

/// <summary>
/// Base class of all service classes.
/// This class has all the neccessary functions to create and send http request to the server.
/// </summary>
internal abstract class ServiceBase : IServiceBase
{
    #region Constructors

    public ServiceBase(
        IHttpClientFactory httpClientFactory,
        IHttpClientPolicy httpClientPolicy,
        IOptions<ServiceOptions> serviceOptions)
    {
        HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        ServiceOptions = serviceOptions?.Value ?? throw new ArgumentNullException(nameof(serviceOptions));
        HttpClientPolicy = httpClientPolicy ?? throw new ArgumentNullException(nameof(httpClientPolicy));
    }

    #endregion

    #region Properties

    protected IHttpClientFactory HttpClientFactory { get; }
    protected IHttpClientPolicy HttpClientPolicy { get; }
    protected ServiceOptions ServiceOptions { get; }

    #endregion

    #region Operations

    protected async Task<TResponse> SendRequestAsync<TResponse>(HttpMethod httpMethod, AsyncRetryPolicy<HttpResponseMessage> retryPolicy, string route, object? body = null)
        where TResponse : class, new()
    {
        var response = new TResponse();

        using (var httpClient = CreateHttpClient())
        {
            var httpRequestMessage = CreateHttpRequestMessage(httpMethod, route, body);
            var httpResponseMessage = await retryPolicy.ExecuteAsync(() => httpClient.SendAsync(httpRequestMessage));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                response = await httpResponseMessage.Content.ReadAsAsync<TResponse>();
            }
            else
            {
                throw new ServiceException(string.Format(Messages.HttpRequestFailed, httpResponseMessage.StatusCode));
            }
        }

        return response;
    }

    private HttpClient CreateHttpClient()
    {
        var httpClient = HttpClientFactory.CreateClient();

        httpClient.BaseAddress = new Uri(ServiceOptions.RequestBaseAddress, UriKind.Absolute);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ServiceOptions.RequestAcceptHeader));
        httpClient.Timeout = ServiceOptions.RequestTimeout;

        return httpClient;
    }
    private HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string route, object? body)
    {
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = httpMethod,
            RequestUri = new Uri($"{ServiceOptions.RequestBaseAddress}/{route}", UriKind.Absolute)
        };

        if (body is not null)
        {
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8);
        }

        httpRequestMessage.Headers.Accept.Clear();
        httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ServiceOptions.RequestAcceptHeader));

        return httpRequestMessage;
    }

    #endregion
}