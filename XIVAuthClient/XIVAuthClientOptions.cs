using XIVAuth.Internal;

namespace XIVAuth
{
    public sealed class XIVAuthClientOptions
    {
        internal XivAuthHelper Helper { get; }

        public XIVAuthClientOptions()
        {
            this.Helper = new(this); // C# why can't I just do this assignment on the property declaration?
        }
        
        public static XIVAuthClientOptions Default { get; } = new();
        public string APIUrl { get; init; } = "https://edge.xivauth.net/api/v1/";
    }
}
