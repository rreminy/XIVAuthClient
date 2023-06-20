using XIVAuth.Models;

namespace XIVAuth.API
{
    public interface IUserAPI
    {
        /// <summary>Get current user information</summary>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="UserModel"/></returns>
        /// <remarks>Minimum required scope: user</remarks>
        public Task<UserModel> GetAsync(CancellationToken cancellationToken = default);
    }
}
