using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    public sealed class UserModel : BaseModel
    {
        [JsonPropertyName("id")]
        public required string Id { get; init; } // Could this be GUID? TODO: Research UUID vs GUID

        [JsonPropertyName("email")]
        public string? Email { get; init; }

        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; init; }

        [JsonPropertyName("social_identities")]
        public IEnumerable<SocialIdentityModel> SocialIdentities { get; init; } = Enumerable.Empty<SocialIdentityModel>();
        
        [JsonPropertyName("mfa_enabled")]
        public required bool MfaEnabled { get; init; }

        [JsonPropertyName("verified_characters")]
        public required bool VerifiedCharacters { get; init; } // HasVerifiedCharacters?

        public override string ToString() => $"{this.Id}: {this.Email}";
    }
}
