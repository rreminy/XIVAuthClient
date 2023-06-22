using XIVAuth.API;

namespace XIVAuth
{
    public interface IXIVAuthUserClient : IDisposable
    {
        public XIVAuthClientOptions Options { get; }

        /// <summary>Characters API</summary>
        public ICharactersAPI Characters { get; }

        /// <summary>User API</summary>
        public IUserAPI User { get; }
    }
}
