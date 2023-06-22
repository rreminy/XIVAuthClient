using XIVAuth.API;

namespace XIVAuth
{
    public interface IXIVAuthUserClient : IDisposable
    {
        public XIVAuthClientOptions Options { get; }
        public ICharactersAPI Characters { get; }
        public IUserAPI User { get; }
    }
}
