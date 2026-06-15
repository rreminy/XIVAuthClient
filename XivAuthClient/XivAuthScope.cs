namespace XivAuth
{
    /// <summary>XIVAuth Scopes</summary>
    public enum XivAuthScope
    {
        /// <summary>Read User Information</summary>
        [XivAuthScopeId("user")]
        User,

        /// <summary>Read User Email</summary>
        [XivAuthScopeId("user:email")]
        UserEmail,

        /// <summary>Read Linked Accounts</summary>
        [XivAuthScopeId("user:social")]
        LinkedAccounts,

        /// <summary>Generate User Claim</summary>
        [XivAuthScopeId("user:jwt")]
        UserJWT,

        /// <summary>Manage User Information</summary>
        [XivAuthScopeId("user:manage")]
        ManageUser,

        /// <summary>Read a Selected Character</summary>
        [XivAuthScopeId("character")]
        Character,

        /// <summary>Read Authorized Characters</summary>
        [XivAuthScopeId("character:all")]
        Characters,

        /// <summary>Generate Character Claims</summary>
        [XivAuthScopeId("character:jwt")]
        CharacterJWT,

        /// <summary>Manage Characters</summary>
        [XivAuthScopeId("character:manage")]
        ManageCharacters,

        /// <summary>Get a refresh token for persistent access</summary>
        [XivAuthScopeId("refresh")]
        Refresh,
    }
}
