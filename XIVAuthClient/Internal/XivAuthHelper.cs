using System.Diagnostics;
using System.Text.Json;
using XIVAuth.Models;

namespace XIVAuth.Internal
{
    internal class XivAuthHelper
    {
        private XIVAuthClientOptions Options { get; }

        public XivAuthHelper(XIVAuthClientOptions options)
        {
            this.Options = options;
        }

        public string GetEndpointUrl(string endpoint) => $"{this.Options.APIUrl}{endpoint}";

        public async Task SendRequestAsync(HttpClient httpClient, HttpMethod method, string endpoint, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var response = await this.SendRequestCoreAsync(httpClient, method, endpoint, content, cancellationToken);
        }

        public async Task<T> SendRequestAsync<T>(HttpClient httpClient, HttpMethod method, string endpoint, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var response = await this.SendRequestCoreAsync(httpClient, method, endpoint, content, cancellationToken);
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken) ?? throw new JsonException();
        }

        private async Task<HttpResponseMessage> SendRequestCoreAsync(HttpClient httpClient, HttpMethod method, string endpoint, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(method, this.GetEndpointUrl(endpoint)) { Content = content };
            var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
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
                    await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                    var errors = await JsonSerializer.DeserializeAsync<ErrorModel>(stream, cancellationToken: cancellationToken);
                    throw new XIVAuthException(errors?.Errors, ex);
                }
                catch (Exception modelException)
                {
#if DEBUG
                    // This exception should in theory never happen,
                    // therefore this is here to debug this exception
                    // during development should it happen.
                    if (Debugger.IsAttached) Debugger.Break();
                    GC.KeepAlive(modelException);
#endif
                    throw new XIVAuthException(null, ex);
                }
            }
        }
    }
}
