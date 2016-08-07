namespace OAuth2.Core.Configuration
{
    /// <summary>
    /// OAuth2 library configuration.
    /// </summary>
    public interface IOAuth2Configuration
    {
        /// <summary>
        /// Returns settings for service client with given name.
        /// </summary>
        IClientConfiguration this[string clientTypeName] { get; }
    }
}