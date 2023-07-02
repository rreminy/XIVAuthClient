using XIVAuth.Internal;
using XIVAuth.Models;

namespace XIVAuth.API
{
    public sealed class UserAPI : IUserAPI
    {
        private IXIVAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XIVAuthClientOptions Options => this.UserClient.Options;
        private XivAuthHelper Helper => this.Options.Helper;

        public UserAPI(IXIVAuthUserClient userClient, HttpClient httpClient)
        {
            this.UserClient = userClient;
            this.HttpClient = httpClient;
        }

        public Task<UserModel> GetAsync(CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<UserModel>(this.HttpClient, HttpMethod.Get, "user", null, cancellationToken);
        }
    }
}
