using System;
using OAuth2.Core.Provider;

namespace OAuth2.Core
{
    public interface IOAuth2ProviderDefinitionService
    {
        OAuth2ProviderDefinition Get(Guid providerId);
    }
}