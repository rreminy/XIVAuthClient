using System.Net.Http.Headers;

namespace XIVAuth
{
    public sealed class XIVAuthClient : IXIVAuthClient, IDisposable
    {
        private readonly bool _ownHandler;
        public XIVAuthClientOptions Options { get; }
        private HttpMessageHandler HttpHandler { get; }

        public XIVAuthClient(XIVAuthClientOptions options) : this(options, new SocketsHttpHandler(), true) { /* Empty */ }

        public XIVAuthClient(XIVAuthClientOptions options, HttpMessageHandler httpHandler, bool ownHandler = true)
        {
            this.Options = options;
            this.HttpHandler = httpHandler;
            this._ownHandler = ownHandler;
        }

        public IXIVAuthUserClient GetUser(string token) => new XIVAuthUserClient(this, this.HttpClientFactory(token));

        private HttpClient HttpClientFactory(string token)
        {
            var httpClient = new HttpClient(this.HttpHandler, false);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        public void Dispose()
        {
            if (this._ownHandler) this.HttpHandler.Dispose();
        }
    }
}
