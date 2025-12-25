using XivAuth.Api;

namespace XivAuth
{
    internal sealed class XivAuthUserClient : IXivAuthUserClient
    {
        private IXivAuthClient Client { get; }
        private HttpClient HttpClient { get; }
        public XivAuthClientOptions Options => this.Client.Options;

        public ICharactersAPI Characters { get; }
        public IUserAPI User { get; }

        public XivAuthUserClient(IXivAuthClient client, HttpClient httpClient)
        {
            this.Client = client;
            this.HttpClient = httpClient;

            this.Characters = new CharactersAPI(this, this.HttpClient);
            this.User = new UserAPI(this, this.HttpClient);
        }

        public void Dispose() => this.HttpClient.Dispose();
    }
}
