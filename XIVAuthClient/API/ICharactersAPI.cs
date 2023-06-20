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
        public Task<CharacterModel> GetAsync(uint lodestoneId, CancellationToken cancellationToken = default);

        /// <summary>Gets all characters</summary>
        /// <param name="token">Cancellation token</param>
        /// <returns>Enumerable of <see cref="CharacterModel"/></returns>
        /// <remarks>Minimum required scope: character</remarks>
        public Task<IEnumerable<CharacterModel>> GetAllAsync(CancellationToken cancellationToken = default);

        public Task<bool> RefreshAsync(uint lodestoneId, CancellationToken cancellationToken = default);

        public Task UpdateAsync(uint lodestoneId, CharacterUpdateModel updateModel, CancellationToken cancellationToken = default); 

        public Task VerifyAsync(uint lodestoneId, CancellationToken cancellationToken = default);

        public Task UnverifyAsync(uint lodestoneId, CancellationToken cancellationToken = default);
    }
}
