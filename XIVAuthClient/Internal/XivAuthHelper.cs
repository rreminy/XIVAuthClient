using System.Text.Json;

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

        public async Task<T> PerformGetAsync<T>(HttpClient httpClient, string endpoint, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync(this.GetEndpointUrl(endpoint), cancellationToken);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken) ?? throw new JsonException();
        }
    }
}
