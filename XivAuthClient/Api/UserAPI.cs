using XivAuth.Internal;
using XivAuth.Models;

namespace XivAuth.Api
{
    public sealed class UserAPI : IUserAPI
    {
        private IXivAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XivAuthClientOptions Options => this.UserClient.Options;
        private XivAuthHelper Helper => this.Options.Helper;

        public UserAPI(IXivAuthUserClient userClient, HttpClient httpClient)
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
