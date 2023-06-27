using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace XIVAuth.Testing
{
    public sealed class AuthCallbackModel
    {
        public string? Code { get; init; }
        public string? Error { get; init; }
        public string? ErrorDescription { get; init; }
    }
}
