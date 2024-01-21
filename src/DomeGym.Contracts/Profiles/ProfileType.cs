using System.Text.Json.Serialization;

namespace DomeGym.Contracts.Profiles;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProfileType { Admin, Trainer, Participant }