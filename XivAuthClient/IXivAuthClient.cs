using System.Net.Http.Headers;

namespace XivAuth
{
    public interface IXivAuthClient : IDisposable
    {
        public XivAuthClientOptions Options { get; }
        public IXivAuthFlowHelper Flows { get; }

        /// <summary>Get an <see cref="IXivAuthUserClient"/> associated by a bearer <paramref name="token"/>.</summary>
        /// <param name="token">OAuth Bearer Token</param>
        /// <returns><see cref="IXivAuthUserClient"/> associated with the bearer <paramref name="token"/>'s user</returns>
        public IXivAuthUserClient GetUser(string token);

        /// <summary>Get an <see cref="IXivAuthUserClient"/> associated by a bearer token</summary>
        /// <param name="authentication">Custom authentication header</param>
        /// <returns><see cref="IXivAuthUserClient"/> associated with the bearer token's user</returns>
        public IXivAuthUserClient GetUser(AuthenticationHeaderValue? authentication);
    }
}
