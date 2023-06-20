using XIVAuth.API;

namespace XIVAuth
{
    internal sealed class XIVAuthUserClient : IXIVAuthUserClient, IDisposable
    {
        private IXIVAuthClient Client { get; }
        private HttpClient HttpClient { get; }
        public XIVAuthClientOptions Options => this.Client.Options;

        public ICharactersAPI Characters { get; }
        public IUserAPI User { get; }

        public XIVAuthUserClient(IXIVAuthClient client, HttpClient httpClient)
        {
            this.Client = client;
            this.HttpClient = httpClient;

            this.Characters = new CharactersAPI(this, this.HttpClient);
            this.User = new UserAPI(this, this.HttpClient);
        }

        public void Dispose() => this.HttpClient.Dispose();
    }
}
