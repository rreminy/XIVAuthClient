using XivAuth.Models;

namespace XivAuth
{
    public interface IXivAuthFlowHelper
    {
        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">Opaque value</param>
        /// <param name="scopes">Scopes</param>
        /// <returns>Authorization URI</returns>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, IEnumerable<string> scopes);

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">Opaque value</param>
        /// <param name="scopes">Scopes, can be space delimited instead of multiple arguments</param>
        /// <returns>Authorization URI</returns>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, params string[] scopes);

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">Opaque value</param>
        /// <param name="scopes">Scopes</param>
        /// <returns>Authorization URI</returns>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, IEnumerable<XivAuthScope> scopes);

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">Opaque value</param>
        /// <param name="scopes">scope</param>
        /// <returns>Authorization URI</returns>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, XivAuthScope[] scopes);

        /// <summary>Gets a bearer token</summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="code">Authorization Code</param>
        /// <param name="redirectUri">Redirect URI</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Bearer Token</returns>
        public Task<TokenInformation> GetTokenAsync(string clientId, string clientSecret, string code, Uri? redirectUri = null, CancellationToken cancellationToken = default);
    }
}
