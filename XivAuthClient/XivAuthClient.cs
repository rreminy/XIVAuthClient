using System.Net.Http.Headers;

namespace XivAuth
{
    public sealed class XivAuthClient : IXivAuthClient
    {
        private readonly bool _ownHandler;
        public XivAuthClientOptions Options { get; }
        public IXivAuthFlowHelper Flows { get; }
        private HttpMessageHandler HttpHandler { get; }

        /// <summary>Construct an <see cref="XivAuthClient"/> with default <see cref="XivAuthClientOptions"/></summary>
        public XivAuthClient() : this(new()) { /* Empty */ }

        /// <summary>Construct an <see cref="XivAuthClient"/> with specified <see cref="XivAuthClientOptions"/></summary>
        /// <param name="options">An <see cref="XivAuthClientOptions"/> configuring this instance's behavior</param>
        public XivAuthClient(XivAuthClientOptions options) : this(options, new SocketsHttpHandler(), true) { /* Empty */ }

        /// <summary>Construct an <see cref="XivAuthClient"/> with specified <see cref="XivAuthClientOptions"/></summary>
        /// <param name="options">An <see cref="XivAuthClientOptions"/> configuring this instance's behavior</param>
        /// <param name="httpHandler"><see cref="HttpMessageHandler"/> to create <see cref="HttpClient"/> with</param>
        /// <param name="ownHandler">Whether this instance owns the <paramref name="httpHandler"/></param>
        public XivAuthClient(XivAuthClientOptions options, HttpMessageHandler httpHandler, bool ownHandler = true)
        {
            this.Options = options;
            this.HttpHandler = httpHandler;
            this._ownHandler = ownHandler;
            this.Flows = new XivAuthFlowHelper(this, new(this.HttpHandler, false));
        }

        /// <inheritdoc/>
        public IXivAuthUserClient GetUser(string token) => this.GetUser(new AuthenticationHeaderValue("Bearer", token));

        /// <inheritdoc/>
        public IXivAuthUserClient GetUser(AuthenticationHeaderValue? authentication) => new XivAuthUserClient(this, this.HttpClientFactory(authentication));

        private HttpClient HttpClientFactory(AuthenticationHeaderValue? authentication)
        {
            var httpClient = new HttpClient(this.HttpHandler, false);
            httpClient.DefaultRequestHeaders.Authorization = authentication;
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public void Dispose()
        {
            if (this._ownHandler) this.HttpHandler.Dispose();
        }
    }
}
