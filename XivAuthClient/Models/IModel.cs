namespace XivAuth.Models
{
    /// <summary>Model binding interface.</summary>
    internal interface IModel
    {
        /// <summary>The time this binding was created.</summary>
        public DateTime CreatedAt { get; }

        /// <summary>The time this binding was last updated.</summary>
        public DateTime UpdatedAt { get; }
    }
}
