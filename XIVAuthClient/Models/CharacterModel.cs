using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    public sealed class CharacterModel : BaseModel
    {
        [JsonPropertyName("persistent_key")]
        public int Key { get; init; } // Persistent key is an SHA1 Hash of User ID + Lodestone ID (+ Secret)

        [JsonPropertyName("lodestone_id")]
        public int LodestoneId { get; init; } // Could be named Id
        
        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("home_world")]
        public required string HomeWorld { get; init; }

        [JsonPropertyName("content_id")] // character:manage
        public required ulong? ContentId { get; init; }
        
        [JsonPropertyName("data_center")]
        public required string DataCenter { get; init; }

        [JsonPropertyName("avatar_url")]
        public required string AvatarUrl { get; init; }

        [JsonPropertyName("portrait_url")]
        public required string PortraitUrl { get; init; }

        [JsonPropertyName("verification_key")] // character:manage
        public required string VerificationKey { get; init; }

        [JsonPropertyName("verified_at")]
        public required DateTime? VerifiedAt { get; init; } // nil seen in XIVAuth source code

        [JsonIgnore]
        public bool Verified => this.VerifiedAt is not null; // Take advantage of the nil
    }
}
