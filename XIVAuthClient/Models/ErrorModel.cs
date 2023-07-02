using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XIVAuth.Models
{
    internal sealed class ErrorModel
    {
        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
    }
}
