using XivAuth.Api;

namespace XivAuth
{
    public interface IXivAuthUserClient : IDisposable
    {
        public XivAuthClientOptions Options { get; }

        /// <summary>Characters API</summary>
        public ICharactersAPI Characters { get; }

        /// <summary>User API</summary>
        public IUserAPI User { get; }
    }
}
