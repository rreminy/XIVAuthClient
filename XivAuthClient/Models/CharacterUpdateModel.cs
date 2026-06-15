using System.Text.Json.Serialization;

namespace XivAuth.Models
{
    public sealed class CharacterUpdateModel
    {
        [JsonPropertyName("content_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ContentId { get; init; }
    }
}
