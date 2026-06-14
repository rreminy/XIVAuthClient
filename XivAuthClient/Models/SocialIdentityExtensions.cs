namespace XivAuth.Models
{
    public static class SocialIdentityExtensions
    {
        extension(SocialIdentityModel social)
        {
            /// <summary>Gets the provider name.</summary>
            public string ProviderName => SocialIdentityUtils.GetProviderName(social.Provider);
        }
    }
}
