using XIVAuth.Internal;

namespace XIVAuth
{
    public sealed class XIVAuthClientOptions
    {
        internal XivAuthHelper Helper { get; }

        public XIVAuthClientOptions()
        {
            this.Helper = new(this); // C# why can't I just do this assignment on the property declaration?
            this.APIUrl = $"{this.BaseUrl}api/v1/";
            this.OAuthUrl = $"{this.BaseUrl}oauth/";
        }

        public static XIVAuthClientOptions Default { get; } = new();
        public string BaseUrl { get; init; } = "https://edge.xivauth.net/";
        public string APIUrl { get; init; }
        public string OAuthUrl { get; init; }
    }
}
