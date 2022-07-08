using Newtonsoft.Json;
using WPFSampleApplication.Service.Abstractions;

namespace WPFSampleApplication.Service.Dtos;

public sealed class PostResponseDto : DtoBase
{
    [JsonProperty("id")]
    public int Id { get; internal set; }

    [JsonProperty("userId")]
    public int UserId { get; internal set; }

    // Title has no use in this application therefore we can use json ignore property instead.
    // [JsonIgnore]
    [JsonProperty("title")]
    public string Title { get; internal set; } = string.Empty;

    // Body has no use in this application therefore we can use json ignore property instead.
    // [JsonIgnore]
    [JsonProperty("body")]
    public string Body { get; internal set; } = string.Empty;
}
