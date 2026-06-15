using System.Net.Http;
using XivAuth.Api;

namespace XivAuth
{
    internal sealed class XivAuthUserClient : IXivAuthUserClient
    {
        private IXivAuthClient Client { get; }
        private HttpClient HttpClient { get; }
        public XivAuthClientOptions Options => this.Client.Options;

        public ICharactersApi Characters { get; }
        public IUserApi User { get; }

        public XivAuthUserClient(IXivAuthClient client, HttpClient httpClient)
        {
            this.Client = client;
            this.HttpClient = httpClient;

            this.Characters = new CharactersApi(this, this.HttpClient);
            this.User = new UserApi(this, this.HttpClient);
        }

        public void Dispose() => this.HttpClient.Dispose();
    }
}
