using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    internal sealed class CharacterRegistrationModel
    {
        [JsonPropertyName("lodestone_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public uint? LodestoneId { get; init; }

        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Name { get; init; }

        [JsonPropertyName("home_world")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? World { get; init; }

    }
}
