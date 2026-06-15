using XivAuth.Api;

namespace XivAuth
{
    public interface IXivAuthUserClient : IDisposable
    {
        public XivAuthClientOptions Options { get; }

        /// <summary>Characters API.</summary>
        public ICharactersApi Characters { get; }

        /// <summary>User API.</summary>
        public IUserApi User { get; }
    }
}
