using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIVAuth.Testing
{
    public sealed class ClientInformation
    {
        public required string ClientId { get; init; }
        public required string ClientSecret { get; init; }
        public required string[] Scopes { get; init; }
    }
}
