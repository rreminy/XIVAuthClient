using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XIVAuth.Models;
using System.Diagnostics.CodeAnalysis;

namespace XIVAuth
{
    [SuppressMessage("Major Code Smell", "S3925")]
    public sealed class XIVAuthException : Exception
    {
        public IEnumerable<XIVAuthErrorModel> Errors { get; }

        public XIVAuthException(IEnumerable<XIVAuthErrorModel> errors)
        {
            this.Errors = errors;
        }
        public XIVAuthException(string error, string description) : this(Enumerable.Repeat(new XIVAuthErrorModel() { Error = error, Description = description }, 1)) { /* Empty */ }
        public XIVAuthException() : this(Enumerable.Empty<XIVAuthErrorModel>()) { /* Empty */ }
    }
}
