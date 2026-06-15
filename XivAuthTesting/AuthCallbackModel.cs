namespace XivAuth.Testing
{
    public sealed class AuthCallbackModel
    {
        public string? Code { get; init; }
        public string? Error { get; init; }
        public string? ErrorDescription { get; init; }
    }
}
