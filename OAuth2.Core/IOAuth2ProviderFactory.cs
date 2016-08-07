using System;
using Hazzik.Maybe;
using OAuth2.Core.Provider;

namespace OAuth2.Core
{
    public interface IOAuth2Manager
    {

        void RegisterOAuth2ProviderType<T>(string providerName) where T : OAuth2ProviderBase;

        Maybe<Uri> GetLoginUri(Guid providerId, Uri redirectUri, string state=null);

        Maybe<UserResourceAccessInfo> Authorize(Guid providerId, Uri redirectUri, string authorizationCode);


    }
}
