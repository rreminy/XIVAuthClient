using System;
using System.Text.Json.Serialization;

namespace XivAuth.Models
{
    public sealed class JwtModel : IEquatable<JwtModel>
    {
        /// <summary>JSON Web Token.</summary>
        [JsonPropertyName("token")]
        public required string Token { get; init; }

        public static bool Equals(JwtModel? left, JwtModel? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Token == right.Token;
        }

        public bool Equals(JwtModel? other) => Equals(this, other);
        public override bool Equals(object? obj) => obj is JwtModel other && Equals(this, other);
        public override int GetHashCode() => this.Token.GetHashCode();
        public override string ToString() => this.Token;
        public static bool operator ==(JwtModel? left, JwtModel? right) => Equals(left, right);
        public static bool operator !=(JwtModel? left, JwtModel? right) => !Equals(left, right);
    }
}
