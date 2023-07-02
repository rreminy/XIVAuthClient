using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XIVAuth
{
    internal sealed class XIVAuthFlowHelper : IXIVAuthFlowHelper
    {
        private IXIVAuthClient Client { get; }
        private XIVAuthClientOptions Options => this.Client.Options;

        internal XIVAuthFlowHelper(IXIVAuthClient client)
        {
            this.Client = client;
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

        public Task<string> GetTokenAsync(string clientId, string clientSecret, string code, Uri redirectUri)
        {
            throw new NotImplementedException();
        }
    }
}
