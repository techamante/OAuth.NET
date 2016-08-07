using RestSharp;

namespace OAuth2.Core
{
    public static class RestResponseExtension
    {

        public static OAuth2Response ToOAuth2Response(this IRestResponse response)
        {
            return new OAuth2Response(response.Content);
        }
    }
}
