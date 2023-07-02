namespace XIVAuth
{
    /// <summary>XIVAuth Scopes</summary>
    public enum XIVAuthScope
    {
        /// <summary>Read User Information</summary>
        [XIVAuthScopeId("user")]
        User,

        /// <summary>Read User Email</summary>
        [XIVAuthScopeId("user:email")]
        UserEmail,

        /// <summary>Read Linked Accounts</summary>
        [XIVAuthScopeId("user:social")]
        LinkedAccounts,

        /// <summary>Generate User Claim</summary>
        [XIVAuthScopeId("user:jwt")]
        UserJWT,

        /// <summary>Manage User Information</summary>
        [XIVAuthScopeId("user:manage")]
        ManageUser,

        /// <summary>Read a Selected Character</summary>
        [XIVAuthScopeId("character")]
        Character,

        /// <summary>Read Authorized Characters</summary>
        [XIVAuthScopeId("character:all")]
        Characters,

        /// <summary>Generate Character Claims</summary>
        [XIVAuthScopeId("character:jwt")]
        CharacterJWT,

        /// <summary>Manage Characters</summary>
        [XIVAuthScopeId("character:manage")]
        ManageCharacters,

        /// <summary>Get a refresh token for persistent access</summary>
        [XIVAuthScopeId("refresh")]
        Refresh,
    }
}
