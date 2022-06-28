using WPFSampleApplication.Service.Abstractions;
using WPFSampleApplication.Service.Dtos;

namespace WPFSampleApplication.Service.Services;

/// <summary>
/// Manages the posts api communication with the server.
/// </summary>
public interface IPostService : IServiceBase
{
    /// <summary>
    /// Geta all the 100 posts from the json placeholder api.
    /// </summary>
    Task<List<PostResponseDto>> GetAllPostsAsync();
}