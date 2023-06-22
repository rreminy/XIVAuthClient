using System.Net.Http.Headers;

namespace XIVAuth
{
    public sealed class XIVAuthClient : IXIVAuthClient
    {
        private readonly bool _ownHandler;
        public XIVAuthClientOptions Options { get; }
        private HttpMessageHandler HttpHandler { get; }

        public XIVAuthClient() : this(new()) { /* Empty */ }

        public XIVAuthClient(XIVAuthClientOptions options) : this(options, new SocketsHttpHandler(), true) { /* Empty */ }

        public XIVAuthClient(XIVAuthClientOptions options, HttpMessageHandler httpHandler, bool ownHandler = true)
        {
            this.Options = options;
            this.HttpHandler = httpHandler;
            this._ownHandler = ownHandler;
        }

        public IXIVAuthUserClient GetUser(string token) => this.GetUser(new AuthenticationHeaderValue("Bearer", token));
        public IXIVAuthUserClient GetUser(AuthenticationHeaderValue? authentication) => new XIVAuthUserClient(this, this.HttpClientFactory(authentication));

        private HttpClient HttpClientFactory(AuthenticationHeaderValue? authentication)
        {
            var httpClient = new HttpClient(this.HttpHandler, false);
            httpClient.DefaultRequestHeaders.Authorization = authentication;
            return httpClient;
        }

        public void Dispose()
        {
            if (this._ownHandler) this.HttpHandler.Dispose();
        }
    }
}
