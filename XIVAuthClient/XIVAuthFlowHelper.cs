using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using XIVAuth.Models;

namespace XIVAuth
{
    internal sealed class XIVAuthFlowHelper : IXIVAuthFlowHelper
    {
        private IXIVAuthClient Client { get; }
        private HttpClient HttpClient { get; }
        private XIVAuthClientOptions Options => this.Client.Options;

        internal XIVAuthFlowHelper(IXIVAuthClient client, HttpClient httpClient)
        {
            this.Client = client;
            this.HttpClient = httpClient;
        }

        /// <inheritdoc/>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, IEnumerable<string> scopes)
        {
            return new($"{this.Options.OAuthUrl}authorize" +
                $"?client_id={HttpUtility.UrlEncode(clientId)}" +
                $"&redirect_uri={HttpUtility.UrlEncode(redirectUri.ToString())}" +
                $"&scope={HttpUtility.UrlEncode(string.Join(' ', scopes))}" +
                (state is null ? string.Empty : $"&state={HttpUtility.UrlEncode(state)}") +
                "&response_type=code");
        }

        /// <inheritdoc/>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, params string[] scopes)
        {
            return this.GetCodeAuthorizationUri(clientId, redirectUri, state, scopes.AsEnumerable());
        }

        /// <inheritdoc/>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, IEnumerable<XIVAuthScope> scopes)
        {
            var scopes2 = scopes.Select(scope => typeof(XIVAuthScope)
                .GetCustomAttribute<XIVAuthScopeIdAttribute>()?.ScopeId ?? throw new ArgumentException($"{nameof(scopes)} contains an invalid scope: {scope}"));
            return this.GetCodeAuthorizationUri(clientId, redirectUri, state, scopes2);
        }

        /// <inheritdoc/>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, XIVAuthScope[] scopes)
        {
            return this.GetCodeAuthorizationUri(clientId, redirectUri, state, scopes.AsEnumerable());
        }

        public async Task<TokenInformation> GetTokenAsync(string clientId, string clientSecret, string code, Uri? redirectUri = null, CancellationToken cancellationToken = default)
        {
            var requestJson = new Dictionary<string, string>()
            {
                {"grant_type", "authorization_code" },
                {"client_id", clientId },
                {"client_secret", clientSecret },
                {"code", code },
            };
            if (redirectUri is not null) requestJson["redirect_uri"] = redirectUri.ToString();
            using var response = await this.HttpClient.PostAsJsonAsync($"{this.Options.OAuthUrl}token", requestJson, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            var tokenInfo = await JsonSerializer.DeserializeAsync<TokenInformation>(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
            Debug.Assert(tokenInfo is not null);
            return tokenInfo;
        }
    }
}
