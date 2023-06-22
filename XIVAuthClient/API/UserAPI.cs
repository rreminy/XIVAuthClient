using XIVAuth.Models;

namespace XIVAuth.API
{
    public sealed class UserAPI : IUserAPI
    {
        private IXIVAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XIVAuthClientOptions Options => this.UserClient.Options;

        public UserAPI(IXIVAuthUserClient userClient, HttpClient httpClient)
        {
            this.UserClient = userClient;
            this.HttpClient = httpClient;
        }

        /// <summary>Gets information about the current authenticated user</summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>User information</returns>
        public Task<UserModel> GetAsync(CancellationToken cancellationToken = default)
        {
            return this.Options.Helper.PerformGetAsync<UserModel>(this.HttpClient, "user", cancellationToken);
        }
    }
}
