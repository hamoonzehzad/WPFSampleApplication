using Newtonsoft.Json;

namespace WPFSampleApplication.Service.Abstractions;

/// <summary>
/// Base class of all multi value converters classes.
/// Only members marked with Newtonsoft.Json.JsonPropertyAttribute or System.Runtime.Serialization.DataMemberAttribute
/// are serialized. This member serialization mode can also be set by marking the
/// class with System.Runtime.Serialization.DataContractAttribute.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public abstract class DtoBase { }
