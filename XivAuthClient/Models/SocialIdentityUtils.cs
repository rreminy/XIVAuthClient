using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Text;

namespace XivAuth.Models
{
    /// <summary>Utility methods for social identities.</summary>
    public static class SocialIdentityUtils
    {
        private static readonly FrozenDictionary<string, string> s_providerIdentifierToNames = new Dictionary<string, string>()
        {
            { "discord", "Discord" },
            { "github", "GitHub" },
            { "steam", "Steam" },
            { "twitch", "Twitch.tv" },
            { "patreon", "Patreon" },
        }.ToFrozenDictionary();

        /// <summary>Gets the <paramref name="provider"/>'s name.</summary>
        /// <param name="provider">Provider identifier.</param>
        /// <returns>Provider name if known, else <paramref name="provider"/>.</returns>
        public static string GetProviderName(string provider)
        {
            if (s_providerIdentifierToNames.TryGetValue(provider, out var name)) return name;
            return provider;
        }
    }
}
