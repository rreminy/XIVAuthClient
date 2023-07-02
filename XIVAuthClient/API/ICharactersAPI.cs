using XIVAuth.Models;

namespace XIVAuth.API
{
    public interface ICharactersAPI
    {
        /// <summary>Gets a specific character with matching Lodestone ID</summary>
        /// <param name="lodestoneId">Lodestone ID</param>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="CharacterModel"/></returns>
        /// <remarks>Minimum required scope: character</remarks>
        public Task<CharacterModel> GetAsync(string lodestoneId, CancellationToken cancellationToken = default);

        /// <summary>Gets all characters</summary>
        /// <param name="token">Cancellation token</param>
        /// <returns>Enumerable of <see cref="CharacterModel"/></returns>
        /// <remarks>Minimum required scope: character</remarks>
        public Task<IEnumerable<CharacterModel>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>Register a character</summary>
        /// <param name="lodestoneId">Lodestone ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns><see cref="CharacterModel"/> or the registered character</returns>
        public Task<CharacterModel> RegisterAsync(string lodestoneId, CancellationToken cancellationToken = default);

        /// <summary>Register a character</summary>
        /// <param name="name">Character name</param>
        /// <param name="world">Character world</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns><see cref="CharacterModel"/> or the registered character</returns>
        public Task<CharacterModel> RegisterAsync(string name, string world, CancellationToken cancellationToken = default);

        /// <summary>Unregister a character</summary>
        /// <param name="lodestoneId">Lodestone ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task representing</returns>
        public Task UnregisterAsync(string lodestoneId, CancellationToken cancellationToken = default);

        /// <summary>Refresh character information</summary>
        /// <param name="lodestoneId">Lodestone ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Character information refresh is enqueued</returns>
        /// <remarks>
        /// Character information can be refreshed once every 24 hours.
        /// It may also be refreshed by other systems.
        /// </remarks>
        public Task<bool> RefreshAsync(string lodestoneId, CancellationToken cancellationToken = default);

        /// <summary>Upddata character information</summary>
        /// <param name="lodestoneId">Lodestone ID</param>
        /// <param name="updateModel">Information to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated character information</returns>
        public Task UpdateAsync<CharacterModel>(string lodestoneId, CharacterUpdateModel updateModel, CancellationToken cancellationToken = default);

        /// <summary>Enqueue an attempt at character verification</summary>
        /// <param name="lodestoneId">Lodestone ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task representing API call progress</returns>
        /// <remarks>
        /// <para>This API only enqueues a request to verify, you must poll the character information to track its progress.</para>
        /// <para>In the future this will return a task ID you can use to track its progress.</para>
        /// <para>Verification can be considered failed after 300 seconds have elapsed</para>
        /// </remarks>
        public Task VerifyAsync(string lodestoneId, CancellationToken cancellationToken = default);
    }
}
