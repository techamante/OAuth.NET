using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Extensions.MonoHttp;

namespace OAuth2.Core
{
    public class OAuth2Response
    {
        private readonly IDictionary<string, string> _collection;

        public string GetValue(string key)
        {
            return _collection[key];
        }
        public OAuth2Response(string content)
        {
            _collection=new Dictionary<string, string>();
            ProcessMessage(content);
        }


        private void ProcessMessage(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;
            try
            {
                var tokens = JObject.Parse(content);
                foreach (var token in tokens)
                {
                    _collection.Add(token.Key, token.ToString());
                }
            }
            catch (JsonReaderException)
            {
                var collection = HttpUtility.ParseQueryString(content);
              
                foreach (var key in collection.AllKeys)
                {
                    _collection.Add(key,collection[key]);
                }
            }
        }
    }
}
