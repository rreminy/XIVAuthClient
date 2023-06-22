using System.Net.Http.Headers;

namespace XIVAuth
{
    public interface IXIVAuthClient : IDisposable
    {
        public XIVAuthClientOptions Options { get; }

        /// <summary>Get an <see cref="IXIVAuthUserClient"/> associated by a bearer token</summary>
        /// <param name="token">OAuth Bearer Token</param>
        /// <returns><see cref="IXIVAuthUserClient"/> associated with the bearer token's user</returns>
        public IXIVAuthUserClient GetUser(string token);

        /// <summary>Get an <see cref="IXIVAuthUserClient"/> associated by a bearer token</summary>
        /// <param name="authentication">Custom authentication header</param>
        /// <returns><see cref="IXIVAuthUserClient"/> associated with the bearer token's user</returns>
        public IXIVAuthUserClient GetUser(AuthenticationHeaderValue? authentication);
    }
}
