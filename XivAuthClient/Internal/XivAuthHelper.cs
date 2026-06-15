using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using XivAuth.Models;

namespace XivAuth.Internal
{
    internal class XivAuthHelper
    {
        private XivAuthClientOptions Options { get; }

        public XivAuthHelper(XivAuthClientOptions options)
        {
            this.Options = options;
        }

        public string GetEndpointUrl(string endpoint) => $"{this.Options.ApiUrl}{endpoint}";

        public async Task SendRequestAsync(HttpClient httpClient, HttpMethod method, string endpoint, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var response = await this.SendRequestCoreAsync(httpClient, method, endpoint, content, cancellationToken).ConfigureAwait(false);
        }

        public async Task<T> SendRequestAsync<T>(HttpClient httpClient, HttpMethod method, string endpoint, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var response = await this.SendRequestCoreAsync(httpClient, method, endpoint, content, cancellationToken).ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<T>(cancellationToken).ConfigureAwait(false) ?? throw new JsonException();
        }

        private async Task<HttpResponseMessage> SendRequestCoreAsync(HttpClient httpClient, HttpMethod method, string endpoint, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(method, this.GetEndpointUrl(endpoint)) { Content = content };
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            await ThrowIfNotSuccess(response, cancellationToken).ConfigureAwait(false);
            return response;
        }

        private static async Task ThrowIfNotSuccess(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                try
                {
                    var errors = await response.Content.ReadFromJsonAsync<ErrorModel>(cancellationToken).ConfigureAwait(false);
                    throw new XivAuthException(errors?.Errors, ex);
                }
                catch (Exception modelException)
                {
                    throw new XivAuthException(null, ex, modelException);
                }
            }
        }
    }
}
