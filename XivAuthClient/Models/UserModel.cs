using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XivAuth.Models
{
    public sealed class UserModel : IRecord
    {
        /// <summary>The UUID of this user. Will be persistent.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("id")]
        public required string Id { get; init; } // Could this be GUID? TODO: Research UUID vs GUID

        /// <summary>The email address of this user.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user:email</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("email")]
        public string? Email { get; init; }

        /// <summary></summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user:email</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("email_verified")]
        public bool? EmailVerified { get; init; }

        /// <summary></summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user:social</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("social_identities")]
        public IEnumerable<SocialIdentityModel>? SocialIdentities { get; init; }

        /// <summary></summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("mfa_enabled")]
        public required bool MfaEnabled { get; init; }

        /// <summary></summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required scope: <c>user</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("verified_characters")]
        public required bool VerifiedCharacters { get; init; } // HasVerifiedCharacters?

        /// <inheritdoc/>
        [JsonPropertyName("created_at")]
        public required DateTime CreatedAt { get; init; }

        /// <inheritdoc/>
        [JsonPropertyName("updated_at")]
        public required DateTime UpdatedAt { get; init; }

        public override string ToString() => $"{this.Id}: {this.Email}";
    }
}
