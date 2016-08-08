using System;
using Newtonsoft.Json.Linq;
using OAuth2.Core;
using OAuth2.Core.Models;
using OAuth2.Core.Provider;
using RestSharp;
using Techamante.Base;

namespace OAuth2.Providers
{
    [OAuth2Provider(OAuth2ProviderType.Facebook)]
    public class FacebookOAuth2Provider : OAuth2ProviderBase
    {
        public FacebookOAuth2Provider(IRequestFactory requestFactory, OAuth2ProviderDefinition oAuth2ProviderDefinition) : base(requestFactory, oAuth2ProviderDefinition)
        {
        
        }

        public override OAuth2Token GetRefreshToken(OAuth2Response oAuth2Response)
        {
            var accessTokenEndPoint = OAuth2ProviderDefinition.AccessTokenEndPoint;
            var client = RequestFactory.CreateClient(accessTokenEndPoint.BaseUri);
            var request = RequestFactory.CreateRequest(accessTokenEndPoint.Resource);
            request.AddObject(new
            {
                grant_type = "fb_exchange_token",
                client_id = OAuth2ProviderDefinition.ClientId,
                client_secret = OAuth2ProviderDefinition.ClientSecret,
                fb_exchange_token = oAuth2Response.GetValue("access_token")
            });
            var response = client.ExecuteAndVerify(request).ToOAuth2Response();
            return GetAccessToken(response);

        }

        public override UserInfo GetUserInfo(OAuth2Token accessToken)
        {
            var userInfoTokenEndPoint = OAuth2ProviderDefinition.UserInfoEndPoint;
            var client = RequestFactory.CreateClient(userInfoTokenEndPoint.BaseUri);
            var request = RequestFactory.CreateRequest(userInfoTokenEndPoint.Resource);
            request.AddObject(new
            {
                access_token = accessToken.Token,
                fields = new string[] { "id", "first_name", "last_name", "email", "picture" }.Concatenate(",")
            });
            var response = client.ExecuteAndVerify(request).ToOAuth2Response();
            return new UserInfo
            {
                Id = response.GetValue("id"),
                FirstName = response.GetValue("first_name"),
                LastName = response.GetValue("last_name")
            };
        }

        public override OAuth2Token RefreshAccessToken(OAuth2Token refreshToken)
        {
            throw new NotImplementedException();
        }

        public override OAuth2Token GetAccessToken(OAuth2Response oAuth2Response)
        {
            var token = new OAuth2Token
            {
                OAuth2TokenType = OAuth2TokenType.AccessToken,
                Token = oAuth2Response.GetValue("access_token")
            };

            int expiresIn;
            if (int.TryParse(oAuth2Response.GetValue("expires"), out expiresIn))
                token.ExpirationDt = DateTime.Now.AddSeconds(expiresIn);

            return token;
        }

        protected override IRestRequest CreateCodeRequest()
        {
            var authorizeEndPoint = OAuth2ProviderDefinition.AuthorizeEndpoint;
            var request = RequestFactory.CreateRequest(authorizeEndPoint.Resource);
            if (string.IsNullOrEmpty(OAuth2ProviderDefinition.ConcatenatedScope))
            {
                request.AddObject(new
                {
                    response_type = "code",
                    client_id = OAuth2ProviderDefinition.ClientId,
                    redirect_uri = OAuth2ProviderDefinition.RedirectUri,
                    State
                });
            }
            else
            {
                request.AddObject(new
                {
                    response_type = "code",
                    client_id = OAuth2ProviderDefinition.ClientId,
                    redirect_uri = OAuth2ProviderDefinition.RedirectUri,
                    scope = OAuth2ProviderDefinition.ConcatenatedScope,
                    State
                });

            }
            return request;
        }

        protected override IRestRequest CreateAuthorizationRequest(string authorizationCode)
        {
            var accessTokenEndpoint = OAuth2ProviderDefinition.AccessTokenEndPoint;
            var request = RequestFactory.CreateRequest(accessTokenEndpoint.Resource);
            request.AddObject(new
            {
                code = authorizationCode,
                client_id = OAuth2ProviderDefinition.ClientId,
                client_secret = OAuth2ProviderDefinition.ClientSecret,
                grant_type = GrantType.AuthorizationCode.GetDisplayText(),
                redirect_uri = OAuth2ProviderDefinition.RedirectUri
            });

            return request;
        }
    }
}