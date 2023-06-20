using System.Text.Json.Serialization;

namespace XIVAuth.Models
{
    public class BaseModel
    {
        [JsonPropertyName("created_at")]
        public required DateTime CreatedAt { get; init; }

        [JsonPropertyName("updated_at")]
        public required DateTime UpdatedAt { get; init; }
    }
}
