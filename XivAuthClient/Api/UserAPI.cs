using XivAuth.Internal;
using XivAuth.Models;

namespace XivAuth.Api
{
    public sealed class UserApi : IUserApi
    {
        private IXivAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XivAuthClientOptions Options => this.UserClient.Options;
        private XivAuthHelper Helper => this.Options.Helper;

        public UserApi(IXivAuthUserClient userClient, HttpClient httpClient)
        {
            this.UserClient = userClient;
            this.HttpClient = httpClient;
        }

        public Task<UserModel> GetAsync(CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<UserModel>(this.HttpClient, HttpMethod.Get, "user", null, cancellationToken);
        }

        public async Task<string> GetJwtAsync(CancellationToken cancellationToken = default)
        {
            var jwt = await this.Helper.SendRequestAsync<JwtModel>(this.HttpClient, HttpMethod.Get, $"user/jwt", null, cancellationToken).ConfigureAwait(false);
            return jwt.Token;
        }
    }
}
