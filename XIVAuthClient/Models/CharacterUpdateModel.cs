using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    public sealed class CharacterUpdateModel
    {
        [JsonPropertyName("content_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public ulong? ContentId { get; init; }
    }
}
