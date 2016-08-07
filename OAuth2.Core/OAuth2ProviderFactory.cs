using System;
using System.Collections.Generic;
using Hazzik.Maybe;
using OAuth2.Core.Provider;
using Techamante.Base;

namespace OAuth2.Core
{
    public class OAuth2Manager : IOAuth2Manager
    {
        private readonly IRequestFactory _requestFactory;
        private readonly IOAuth2ProviderDefinitionService _oAuth2ProviderDefinitionService;

        private readonly IDictionary<string, Type> _providerTypes;

        public OAuth2Manager(
            IRequestFactory requestFactory,
            IOAuth2ProviderDefinitionService oAuth2ProviderDefinitionService)
        {
            _requestFactory = requestFactory;
            _oAuth2ProviderDefinitionService = oAuth2ProviderDefinitionService;
            _providerTypes = new Dictionary<string, Type>();
        }


        public void RegisterOAuth2ProviderType<T>(string providerName) where T : OAuth2ProviderBase => _providerTypes.Add(providerName, typeof(T));
       
        public Maybe<Uri> GetLoginUri(Guid providerId, Uri redirectUri, string state = null)
        {
            var oAuth2Provider = GetOAuth2Provider(providerId, redirectUri);
            return (oAuth2Provider.HasValue)
                ? oAuth2Provider.Value.GenerateLoginUri(state)
                : Maybe<Uri>.Nothing;
        }

        public Maybe<UserResourceAccessInfo> Authorize(Guid providerId, Uri redirectUri, string authorizationCode)
        {
            var oAuth2Provider = GetOAuth2Provider(providerId, redirectUri);
            if(!oAuth2Provider.HasValue) return Maybe<UserResourceAccessInfo>.Nothing;

            var resourceAccessInfo = new UserResourceAccessInfo();

            var oAuth2Response = oAuth2Provider.Value.Authorize(authorizationCode);
            resourceAccessInfo.AccessToken = oAuth2Provider.Value.GetAccessToken(oAuth2Response);
            resourceAccessInfo.UserInfo = oAuth2Provider.Value.GetUserInfo(resourceAccessInfo.AccessToken);
            resourceAccessInfo.RefreshToken = oAuth2Provider.Value.GetRefreshToken(oAuth2Response);

            return resourceAccessInfo;
        }


        private Maybe<IOAuth2Provider> GetOAuth2Provider(Guid providerId, Uri redirectUri)
        {
            var providerDefinition = _oAuth2ProviderDefinitionService.Get(providerId);
            providerDefinition.RedirectUri = redirectUri;
            var providerType = _providerTypes[providerDefinition.Name];
            return providerType == null
                ? Maybe<IOAuth2Provider>.Nothing
                : new Maybe<IOAuth2Provider>((OAuth2ProviderBase)Activator.CreateInstance(providerType, _requestFactory, providerDefinition));
        }
    }
}