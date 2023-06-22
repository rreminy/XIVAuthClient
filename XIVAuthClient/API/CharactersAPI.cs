using System.Diagnostics;
using System.Net;
using XIVAuth.Models;

namespace XIVAuth.API
{
    internal sealed class CharactersAPI : ICharactersAPI
    {
        private IXIVAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XIVAuthClientOptions Options => this.UserClient.Options;

        public CharactersAPI(IXIVAuthUserClient userClient, HttpClient httpClient)
        {
            this.UserClient = userClient;
            this.HttpClient = httpClient;
        }

        public Task<CharacterModel> GetAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Options.Helper.PerformGetAsync<CharacterModel>(this.HttpClient, $"characters/{lodestoneId}", cancellationToken);
        }

        public Task<IEnumerable<CharacterModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return this.Options.Helper.PerformGetAsync<IEnumerable<CharacterModel>>(this.HttpClient, $"characters", cancellationToken);
        }

        public async Task<bool> RefreshAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            var response = await this.HttpClient.PostAsync(this.Options.Helper.GetEndpointUrl($"characters/{lodestoneId}/refresh"), null, cancellationToken);
            Debug.Assert(response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.UnprocessableEntity);
            return response.StatusCode == HttpStatusCode.Accepted; //422 for false
        }

        public Task UpdateAsync(uint lodestoneId, CharacterUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            // TODO: PUT|PATCH /characters/{lodestone_id}
            /*
             * Scopes required: character:manage
             * 
             * This API route allows updating a specific character’s entry. It takes a body consisting of
             * a JSON object with the fields to update:
             * {
             *   "content_id": "1234567890123"
             * }
             * Only the following fields can be edited: content_id
             */

            // TODO: What does this return?
            throw new NotImplementedException();
        }

        public Task VerifyAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            // TODO: POST /characters/{lodestone_id}/verify
            // Status code 202 => return
            // Other status => throw
            /*
             * Scopes required: character:manage
             * 
             * This API route will trigger an asynchronous verification attempt for the specified character.
             * 
             * Returns HTTP code 202 if the verification request was successfully enqueued. API endpoints
             * are suggested to poll GET /characters/{lodestone_id} for updates. A verification attempt may
             * be considered “failed” if the character still remains unverified after 300 seconds.
             * 
             * A future version of this API will return a Task ID which can be used to retrieve information
             * about the verification task’s current status.
             */

            throw new NotImplementedException();
        }

        public Task UnverifyAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            // TODO: DELETE /characters/{lodestone_id}/verify
            // Do we really want this?

            throw new NotImplementedException($"Banned reason: VIP - Executing the {nameof(UnverifyAsync)} method");
        }
    }
}
