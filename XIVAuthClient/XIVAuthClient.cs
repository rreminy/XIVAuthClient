using System.Net.Http.Headers;
using System.Reflection;
using System.Web;

namespace XIVAuth
{
    public sealed class XIVAuthClient : IXIVAuthClient
    {
        private readonly bool _ownHandler;
        public XIVAuthClientOptions Options { get; }
        public IXIVAuthFlowHelper Flows { get; }
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
            this.Flows = new XIVAuthFlowHelper(this);
            this.HttpHandler = httpHandler;
            this._ownHandler = ownHandler;
        }

        /// <inheritdoc/>
        public IXIVAuthUserClient GetUser(string token) => this.GetUser(new AuthenticationHeaderValue("Bearer", token));

        /// <inheritdoc/>
        public IXIVAuthUserClient GetUser(AuthenticationHeaderValue? authentication) => new XIVAuthUserClient(this, this.HttpClientFactory(authentication));

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
