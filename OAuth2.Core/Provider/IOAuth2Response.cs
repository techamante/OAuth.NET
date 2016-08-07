using System.Collections.Generic;

namespace OAuth2.Core.Provider
{
    public interface IOAuth2Response
    {
        IDictionary<string, string> Processresponse(string content);
    }
}