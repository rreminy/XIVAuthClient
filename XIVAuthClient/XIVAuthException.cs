using System.Diagnostics.CodeAnalysis;

namespace XIVAuth
{
    [SuppressMessage("Major Code Smell", "S3925")]
    public sealed class XIVAuthException : Exception
    {
        /// <summary>Errors list</summary>
        public IEnumerable<string> Errors { get; }

        public XIVAuthException(string? message = null, Exception? innerException = null, IEnumerable<string>? errors = null)
            : base(message ?? DeriveMessageFromErrors(errors), innerException)
        {
            this.Errors = errors ?? Enumerable.Empty<string>();
        }

        public XIVAuthException(IEnumerable<string>? errors, Exception? innerException = null) : this(null, innerException, errors) { /* Empty */ }

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
