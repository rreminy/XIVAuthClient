using System.Text.Json.Serialization;

namespace XivAuth.Models
{
    internal sealed class ErrorModel
    {
        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; init; } = [];
    }
}
