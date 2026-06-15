namespace XivAuth.Testing
{
    public sealed class ClientInformation
    {
        public required string ClientId { get; init; }
        public required string ClientSecret { get; init; }
        public required string[] Scopes { get; init; }
    }
}
