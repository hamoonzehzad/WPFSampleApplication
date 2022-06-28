using Microsoft.Extensions.Options;
using WPFSampleApplication.Service.Abstractions;
using WPFSampleApplication.Service.Configurations.Service;
using WPFSampleApplication.Service.Dtos;
using WPFSampleApplication.Service.Exceptions;
using WPFSampleApplication.Service.Policies;
using WPFSampleApplication.Service.Resources;

namespace WPFSampleApplication.Service.Services;

/// <summary>
/// Manages the posts api communication with the server.
/// </summary>
internal sealed class PostService : ServiceBase, IPostService
{
    public PostService(
        IHttpClientFactory httpClientFactory,
        IHttpClientPolicy httpClientPolicy,
        IOptions<ServiceOptions> serviceOptions)
        : base(httpClientFactory, httpClientPolicy, serviceOptions)
    { }

    /// <summary>
    /// Geta all the 100 posts from the json placeholder api.
    /// </summary>
    public async Task<List<PostResponseDto>> GetAllPostsAsync()
    {
        var postResponseDtoList = await SendRequestAsync<List<PostResponseDto>>(HttpMethod.Get, HttpClientPolicy.LinearRetryPolicy, "posts");

        return postResponseDtoList;
    }
}
