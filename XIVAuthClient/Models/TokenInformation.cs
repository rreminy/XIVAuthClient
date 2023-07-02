using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    public sealed class TokenInformation
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; init; }

        [JsonPropertyName("token_type")]
        public required string TokenType { get; init; }

        [JsonPropertyName("expires_in")]
        public required int ExpiresIn { get; init; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; init; }

        [JsonPropertyName("scope")]
        public required string Scopes { get; init; }

        [JsonPropertyName("created_at")]
        public required int CreatedAt { get; init; }

        [JsonIgnore]
        public int ExpiresAt => this.CreatedAt + this.ExpiresIn;

        public AuthenticationHeaderValue GetAuthenticationHeader() => new(this.TokenType, this.AccessToken);
    }
}