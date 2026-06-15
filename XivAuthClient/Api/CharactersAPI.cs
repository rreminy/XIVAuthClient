using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using XivAuth.Internal;
using XivAuth.Models;

namespace XivAuth.Api
{
    internal sealed class CharactersApi : ICharactersApi
    {
        private IXivAuthUserClient UserClient { get; }
        private HttpClient HttpClient { get; }
        private XivAuthClientOptions Options => this.UserClient.Options;
        private XivAuthHelper Helper => this.Options.Helper;

        public CharactersApi(IXivAuthUserClient userClient, HttpClient httpClient)
        {
            this.UserClient = userClient;
            this.HttpClient = httpClient;
        }

        public Task<CharacterModel> GetAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<CharacterModel>(this.HttpClient, HttpMethod.Get, $"characters/{lodestoneId}", null, cancellationToken);
        }

        public Task<IEnumerable<CharacterModel>> GetAllAsync(string? name = null, string? homeWorld = null, string? dataCenter = null, CancellationToken cancellationToken = default)
        {
            if (name is null && homeWorld is null && dataCenter is null) return this.Helper.SendRequestAsync<IEnumerable<CharacterModel>>(this.HttpClient, HttpMethod.Get, "characters", null, cancellationToken);
            var query = new List<string>();
            if (name is not null) query.Add($"name={Uri.EscapeDataString(name)}");
            if (homeWorld is not null) query.Add($"home_world={Uri.EscapeDataString(homeWorld)}");
            if (dataCenter is not null) query.Add($"data_center={Uri.EscapeDataString(dataCenter)}");
            return this.Helper.SendRequestAsync<IEnumerable<CharacterModel>>(this.HttpClient, HttpMethod.Get, $"characters?{string.Join('&', query)}", null, cancellationToken);
        }

        public Task<CharacterModel> RegisterAsync(uint lodestoneId, CancellationToken cancellationToken = default)
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

        public Task UnregisterAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync(this.HttpClient, HttpMethod.Delete, $"characters/{lodestoneId}", null, cancellationToken);
        }

        public async Task<bool> RefreshAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            var response = await this.HttpClient.PostAsync(this.Options.Helper.GetEndpointUrl($"characters/{lodestoneId}/refresh"), null, cancellationToken);
            Debug.Assert(response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.UnprocessableEntity);
            return response.StatusCode == HttpStatusCode.Accepted; //422 for false
        }

        public Task UpdateAsync<CharacterModel>(uint lodestoneId, CharacterUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync<CharacterModel>(this.HttpClient, HttpMethod.Patch, $"characters/{lodestoneId}", JsonContent.Create(updateModel), cancellationToken);
        }

        public Task VerifyAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync(this.HttpClient, HttpMethod.Post, $"characters/{lodestoneId}/verify", null, cancellationToken);
        }

        public Task UnverifyAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            return this.Helper.SendRequestAsync(this.HttpClient, HttpMethod.Delete, $"characters/{lodestoneId}/verify", null, cancellationToken);
        }

        public async Task<string> GetJwtAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            var jwt = await this.Helper.SendRequestAsync<JwtModel>(this.HttpClient, HttpMethod.Get, $"characters/{lodestoneId}/jwt", null, cancellationToken).ConfigureAwait(false);
            return jwt.Token;
        }

        public async Task<object?> GetLodestoneAsync(uint lodestoneId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException($"characters/{lodestoneId}/lodestone");
        }
    }
}
