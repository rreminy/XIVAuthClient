using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace XivAuth
{
    [SuppressMessage("Major Code Smell", "S3925")]
    public sealed class XivAuthException : Exception
    {
        /// <summary>Errors list.</summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>Error model exception, if any.</summary>
        public Exception? ModelException { get; }

        public XivAuthException(string? message = null, Exception? innerException = null, IEnumerable<string>? errors = null, Exception? modelException = null)
            : base(message ?? DeriveMessageFromErrors(errors), innerException)
        {
            this.Errors = errors ?? [];
            this.ModelException = modelException;
        }

        public XivAuthException(IEnumerable<string>? errors, Exception? innerException = null, Exception? modelException = null) : this(null, innerException, errors, modelException) { /* Empty */ }

        private static string? DeriveMessageFromErrors(IEnumerable<string>? errors)
        {
            if (errors is not null && errors.TryGetNonEnumeratedCount(out var count))
            {
                if (count == 1) return errors.First();
                if (count >= 2) return $"Multiple {nameof(Errors)}";
            }
            return null;
        }
    }
}
