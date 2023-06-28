using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XIVAuth
{
    public interface IXIVAuthFlowHelper
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
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, IEnumerable<XIVAuthScope> scopes);

        /// <summary>Gets an authorization <see cref="Uri"/></summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="redirectUri">Redirect Uri</param>
        /// <param name="state">Opaque value</param>
        /// <param name="scopes">scope</param>
        /// <returns>Authorization URI</returns>
        public Uri GetCodeAuthorizationUri(string clientId, Uri redirectUri, string? state, XIVAuthScope[] scopes);
    }
}
