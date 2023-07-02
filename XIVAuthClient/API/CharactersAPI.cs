using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using XIVAuth.Internal;
using XIVAuth.Models;

namespace XIVAuth.API
{
    internal sealed class CharactersAPI : ICharactersAPI
    {
        private IXIVAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XIVAuthClientOptions Options => this.UserClient.Options;
        private XivAuthHelper Helper => this.Options.Helper;

        public CharactersAPI(IXIVAuthUserClient userClient, HttpClient httpClient)
        {
            this.UserClient = userClient;
            this.HttpClient = httpClient;
        }

        public Task<CharacterModel> GetAsync(string lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<CharacterModel>(this.HttpClient, HttpMethod.Get, $"characters/{lodestoneId}", null, cancellationToken);
        }

        public Task<IEnumerable<CharacterModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<IEnumerable<CharacterModel>>(this.HttpClient, HttpMethod.Get, "characters", null, cancellationToken);
        }

        public Task<CharacterModel> RegisterAsync(string lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.RegisterAsyncCore(new() { LodestoneId = lodestoneId }, cancellationToken);
        }

        public Task<CharacterModel> RegisterAsync(string name, string world, CancellationToken cancellationToken = default)
        {
            return this.RegisterAsyncCore(new() { Name = name, World = world }, cancellationToken);
        }

        private Task<CharacterModel> RegisterAsyncCore(CharacterRegistrationModel registration, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<CharacterModel>(this.HttpClient, HttpMethod.Post, "characters", JsonContent.Create(registration), cancellationToken);
        }

        public Task UnregisterAsync(string lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync(this.HttpClient, HttpMethod.Delete, $"characters/{lodestoneId}", null, cancellationToken);
        }

        public async Task<bool> RefreshAsync(string lodestoneId, CancellationToken cancellationToken = default)
        {
            var response = await this.HttpClient.PostAsync(this.Options.Helper.GetEndpointUrl($"characters/{lodestoneId}/refresh"), null, cancellationToken);
            Debug.Assert(response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.UnprocessableEntity);
            return response.StatusCode == HttpStatusCode.Accepted; //422 for false
        }

        public Task UpdateAsync<CharacterModel>(string lodestoneId, CharacterUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<CharacterModel>(this.HttpClient, HttpMethod.Patch, $"characters/{lodestoneId}", JsonContent.Create(updateModel), cancellationToken);
        }

        public Task VerifyAsync(string lodestoneId, CancellationToken cancellationToken = default)
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
    }
}
