using XivAuth.Internal;

namespace XivAuth
{
    /// <summary>XIVAuth Client Options.</summary>
    public sealed class XivAuthClientOptions
    {
        /// <summary>Helper methods.</summary>
        internal XivAuthHelper Helper { get; }

        /// <summary>Initializes a new <see cref="XivAuthClientOptions"/>.</summary>
        public XivAuthClientOptions() : this("https://xivauth.net/") { /* Empty */ }

        /// <summary>Initializes a new <see cref="XivAuthClientOptions"/> with a specified <paramref name="baseUrl"/>.</summary>
        /// <param name="baseUrl">XIVAuth's base URL.</param>
        /// <remarks><see cref="ApiUrl"/> and <see cref="OAuthUrl"/> will automatically be derived from <paramref name="baseUrl"/></remarks>
        public XivAuthClientOptions(string baseUrl) : this(baseUrl, $"{baseUrl}api/v1/", $"{baseUrl}oauth/") { /* Empty */ }

        /// <summary>Initializes a new <see cref="XivAuthClientOptions"/> with a specified <paramref name="baseUrl"/>, <paramref name="apiUrl"/> and <paramref name="oAuthUrl"/>.</summary>
        /// <param name="baseUrl">XIVAuth's base URL.</param>
        /// <param name="apiUrl">XIVAuth's API URL.</param>
        /// <param name="oAuthUrl">XIVAuth's OAuth URL.</param>
        public XivAuthClientOptions(string baseUrl, string apiUrl, string oAuthUrl)
        {
            this.Helper = new(this); // C# why can't I just do this assignment on the property declaration?
            this.BaseUrl = baseUrl;
            this.ApiUrl = apiUrl;
            this.OAuthUrl = oAuthUrl;
        }

        /// <summary>Default <see cref="XivAuthClientOptions"/>.</summary>
        public static XivAuthClientOptions Default { get; } = new();

        /// <summary>XIVAuth Base URL.</summary>
        public string BaseUrl { get; init; }

        /// <summary>XIVAuth API URL.</summary>
        public string ApiUrl { get; init; }

        /// <summary>XIVAuth OAuth URL.</summary>
        public string OAuthUrl { get; init; }

        public override string ToString() => $"{this.BaseUrl} (API: {this.ApiUrl} | OAuth: {this.OAuthUrl})";
    }
}
