using System;
using OAuth2.Core.Models;
using RestSharp;
using Techamante.Base;

namespace OAuth2.Core.Provider
{
    /// <summary>
    /// Base class for OAuth2 client implementation.
    /// </summary>
    public abstract class OAuth2ProviderBase : IOAuth2Provider
    {

        protected readonly IRequestFactory RequestFactory;
        protected readonly OAuth2ProviderDefinition OAuth2ProviderDefinition;

        public string State { get; private set; }

        protected OAuth2ProviderBase(
            IRequestFactory requestFactory,
            OAuth2ProviderDefinition oAuth2ProviderDefinition)
        {
            RequestFactory = requestFactory;
            OAuth2ProviderDefinition = oAuth2ProviderDefinition;

        }


        public virtual Uri GenerateLoginUri(string state = null)
        {
            var authorizeEndPoint = OAuth2ProviderDefinition.AuthorizeEndpoint;
            var client = RequestFactory.CreateClient(authorizeEndPoint.BaseUri);
            var request = CreateCodeRequest();     
            return client.BuildUri(request);
        }

        public virtual OAuth2Response Authorize(string authorizationCode)
        {
            var accessTokenEndPoint = OAuth2ProviderDefinition.AccessTokenEndPoint;
            var client = RequestFactory.CreateClient(accessTokenEndPoint.BaseUri);
            var request = CreateAuthorizationRequest(authorizationCode);
            var response = client.ExecuteAndVerify(request); 
            return new OAuth2Response(response.Content);
        }


        public abstract UserInfo GetUserInfo(OAuth2Token accessToken);

        public abstract OAuth2Token RefreshAccessToken(OAuth2Token refreshToken);
      
        public abstract OAuth2Token GetAccessToken(OAuth2Response oAuth2Response);

        public abstract OAuth2Token GetRefreshToken(OAuth2Response oAuth2Response);

        protected abstract IRestRequest CreateCodeRequest();

        protected abstract IRestRequest CreateAuthorizationRequest(string authorizationCod);


    }
}