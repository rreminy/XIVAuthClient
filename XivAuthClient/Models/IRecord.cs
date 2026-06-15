namespace XivAuth.Models
{
    /// <summary>Record binding interface.</summary>
    internal interface IRecord
    {
        /// <summary>The time this binding was created.</summary>
        public DateTime CreatedAt { get; }

        /// <summary>The time this binding was last updated.</summary>
        public DateTime UpdatedAt { get; }
    }
}
