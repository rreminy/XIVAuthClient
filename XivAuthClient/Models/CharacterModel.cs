using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace XivAuth.Models
{
    /// <summary>Represents a Character registration.</summary>
    public sealed class CharacterModel : IRecord
    {
        /// <summary>Internal ID for this character’s binding to the user. Will change if the character is removed from the user’s account.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [JsonPropertyName("__crid")]
        public string? __CrId { get; init; }

        /// <summary>Internal ID for the globally-tracked character. Persistent across users, but may change if the character has been fully deleted from the XIVAuth database.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [JsonPropertyName("__cid")]
        public string? __CId { get; init; }

        /// <summary>An opaque value representing this character/user combination. Guaranteed to remain consistent so long as the character is associated with the same user, even if the character is removed and re-added.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("persistent_key")]
        public required string PersistentKey { get; init; } // HMAC(user_id + lodestone_id, backend_secret)

        /// <summary>The ID of this character on FFXIV’s Lodestone service.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("lodestone_id")]
        public required uint LodestoneId { get; init; } // Could be named Id (NOTE: Passed as string)

        /// <summary>The last seen/reported name for this character according to Lodestone.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("name")]
        public required string Name { get; init; }

        /// <summary>The (English) name of this character’s home world according to Lodestone.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("home_world")]
        public required string HomeWorld { get; init; }

        /// <summary>The (English) name of this character’s data center according to Lodestone.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("data_center")]
        public required string DataCenter { get; init; }

        /// <summary>The reported content ID of this specific character. <b>This value is not verified in any way</b> and primarily exists to make searches easier for certain clients.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character:manage</c></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("content_id")] // character:manage
        public string? ContentId { get; init; } // NOTE: Passed as string

        /// <summary>An avatar/headshot image for this character. May return either a Lodestone URL or a CDN URL.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("avatar_url")]
        public required string AvatarUrl { get; init; }

        /// <summary>A full-body portrait of this character. May return either a Lodestone URL or a CDN URL.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("portrait_url")]
        public required string PortraitUrl { get; init; }

        /// <summary>A boolean flag indicating whether the character has been verified.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character:manage</c>.</item>
        /// <item>This property checks for <see cref="VerifiedAt"/> not being <see langword="null"/> and therefore only <c>character</c> scope is needed.</item>
        /// </list>
        /// </remarks>
        [JsonIgnore] // verified
        public bool Verified => this.VerifiedAt is not null; // Take advantage of the nil: https://github.com/KazWolfe/XIVAuth/blob/b8ba97e3ae2b5d38ee7c9174fdcac07db2953e2d/app/models/character_registration.rb#L41

        /// <summary>The verification key that must be placed in the character’s Lodestone page.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character:manage</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("verification_key")]
        public string? VerificationKey { get; init; }

        /// <summary>The time this character was verified, if any. If <c>null</c>, the character is not verified.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("verified_at")]
        public DateTime? VerifiedAt { get; init; } // nil seen in XIVAuth source code

        /// <summary>The time this character binding was created.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("created_at")]
        public required DateTime CreatedAt { get; init; } // nil seen in XIVAuth source code

        /// <summary>The time this character binding was last updated.</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>character</c>.</item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("updated_at")]
        public required DateTime UpdatedAt { get; init; } // nil seen in XIVAuth source code

        public override string ToString() => $"{this.Name} <{this.HomeWorld}>";
    }
}
