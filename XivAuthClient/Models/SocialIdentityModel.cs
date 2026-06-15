using System;
using System.Collections.Frozen;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace XivAuth.Models
{
    public sealed class SocialIdentityModel : IRecord
    {
        /// <summary>An identifier for the provider of this social identity.</summary>
        [JsonPropertyName("provider")]
        public required string Provider { get; init; }

        /// <summary>The ID for this user from the external service.</summary>
        [JsonPropertyName("external_id")]
        public required string ExternalId { get; init; }

        /// <summary>The best known name for this service. Generally an account display name or real name.</summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>The best known nickname for this service. Generally will be the account’s username or login name.</summary>
        [JsonPropertyName("nickname")]
        public string? Nickname { get; init; }

        /// <summary>The email address of the user, according to the external service. May not match the user’s XIVAuth email.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user:email</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("email")]
        public string? Email { get; init; }

        /// <inheritdoc/>
        [JsonPropertyName("created_at")]
        public required DateTime CreatedAt { get; init; } // nil seen in XIVAuth source code

        /// <inheritdoc/>
        [JsonPropertyName("updated_at")]
        public required DateTime UpdatedAt { get; init; } // nil seen in XIVAuth source code

        public override string ToString() => $"{this.Provider}: {this.ExternalId}";
    }
}
