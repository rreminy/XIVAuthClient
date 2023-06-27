using System.Net.Http.Headers;
using System.Reflection;
using System.Web;

namespace XIVAuth
{
    public sealed class XIVAuthClient : IXIVAuthClient
    {
        private readonly bool _ownHandler;
        public XIVAuthClientOptions Options { get; }
        private HttpMessageHandler HttpHandler { get; }

        /// <summary>Construct an <see cref="XIVAuthClient"/> with default <see cref="XIVAuthClientOptions"/></summary>
        public XIVAuthClient() : this(new()) { /* Empty */ }

        /// <summary>Construct an <see cref="XIVAuthClient"/> with specified <see cref="XIVAuthClientOptions"/></summary>
        /// <param name="options">An <see cref="XIVAuthClientOptions"/> configuring this instance's behavior</param>
        public XIVAuthClient(XIVAuthClientOptions options) : this(options, new SocketsHttpHandler(), true) { /* Empty */ }

        /// <summary>Construct an <see cref="XIVAuthClient"/> with specified <see cref="XIVAuthClientOptions"/></summary>
        /// <param name="options">An <see cref="XIVAuthClientOptions"/> configuring this instance's behavior</param>
        /// <param name="httpHandler"><see cref="HttpMessageHandler"/> to create <see cref="HttpClient"/> with</param>
        /// <param name="ownHandler">Whether this instance owns the <paramref name="httpHandler"/></param>
        public XIVAuthClient(XIVAuthClientOptions options, HttpMessageHandler httpHandler, bool ownHandler = true)
        {
            this.Options = options;
            this.HttpHandler = httpHandler;
            this._ownHandler = ownHandler;
        }

        /// <inheritdoc/>
        public IXIVAuthUserClient GetUser(string token) => this.GetUser(new AuthenticationHeaderValue("Bearer", token));

        /// <inheritdoc/>
        public IXIVAuthUserClient GetUser(AuthenticationHeaderValue? authentication) => new XIVAuthUserClient(this, this.HttpClientFactory(authentication));

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="scopes">Scopes</param>
        /// <returns>Authorization URI</returns>
        public Uri GetAuthorizationUri(string clientId, Uri redirectUri, string nonce, IEnumerable<string> scopes)
        {
            return new($"{this.Options.OAuthUrl}authorize" +
                $"?client_id={HttpUtility.UrlEncode(clientId)}" +
                $"&redirect_uri={HttpUtility.UrlEncode(redirectUri.ToString())}" +
                $"&scope={HttpUtility.UrlEncode(string.Join(' ', scopes))}" +
                //$"&nonce={HttpUtility.UrlEncode(nonce)}" +
                "&response_type=code");
        }

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="scopes">Scopes, can be space delimited instead of multiple arguments</param>
        /// <returns>Authorization URI</returns>
        public Uri GetAuthorizationUri(string clientId, Uri redirectUri, string nonce, params string[] scopes)
        {
            return this.GetAuthorizationUri(clientId, redirectUri, nonce, scopes.AsEnumerable());
        }

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="scopes">Scopes</param>
        /// <returns>Authorization URI</returns>
        public Uri GetAuthorizationUri(string clientId, Uri redirectUri, string nonce, IEnumerable<XIVAuthScope> scopes)
        {
            var scopes2 = scopes.Select(scope => typeof(XIVAuthScope).GetCustomAttribute<XIVAuthScopeIdAttribute>()?.ScopeId ?? throw new ArgumentException($"{nameof(scopes)} contains an invalid scope"));
            return this.GetAuthorizationUri(clientId, redirectUri, nonce, scopes2);
        }

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="scopes">scope</param>
        /// <returns>Authorization URI</returns>
        public Uri GetAuthorizationUri(string clientId, Uri redirectUri, string nonce, XIVAuthScope[] scopes)
        {
            return this.GetAuthorizationUri(clientId, redirectUri, nonce, scopes.AsEnumerable());
        }

        private HttpClient HttpClientFactory(AuthenticationHeaderValue? authentication)
        {
            var httpClient = new HttpClient(this.HttpHandler, false);
            httpClient.DefaultRequestHeaders.Authorization = authentication;
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            return httpClient;
        }

        public void Dispose()
        {
            if (this._ownHandler) this.HttpHandler.Dispose();
        }
    }
}
