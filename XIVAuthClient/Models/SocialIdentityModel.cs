using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    public sealed class SocialIdentityModel : BaseModel
    {
        [JsonPropertyName("provider")]
        public required string Provider { get; init; }

        [JsonPropertyName("external_id")]
        public required string ExternalId { get; init; }

        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("nickname")]
        public string? Nickname { get; init; }

        [JsonPropertyName("email")]
        public string? Email { get; init; }

        public override string ToString() => $"{this.Provider}: {this.ExternalId}";
    }
}
