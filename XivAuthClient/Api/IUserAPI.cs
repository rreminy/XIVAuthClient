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
    }
}
