using System.Threading;
using System.Threading.Tasks;
using XivAuth.Models;

namespace XivAuth.Api
{
    public interface IUserApi
    {
        /// <summary>Get current user information</summary>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="UserModel"/></returns>
        /// <remarks>Minimum required scope: user</remarks>
        public Task<UserModel> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>Get JWT Attestation for this user.</summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>A JWT Representing this user.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>Required Scope: <c>user:jwt</c>.</item>
        /// </list>
        /// </remarks>
        public Task<string> GetJwtAsync(CancellationToken cancellationToken = default);
    }
}
