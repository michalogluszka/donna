using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Donna.Core.AzureSecurity
{
     /*
     * This class demonstrates how to get a valid access token.
     */
    public class CognitiveServicesAuthentication
    {
        public static readonly string FetchTokenUri =
            "https://northeurope.api.cognitive.microsoft.com/sts/v1.0/issueToken";
        private string _subscriptionKey;
        private string _token;

        public CognitiveServicesAuthentication(string subscriptionKey)
        {
            this._subscriptionKey = subscriptionKey;
            this._token = FetchTokenAsync(FetchTokenUri, subscriptionKey).Result;
        }

        public string GetAccessToken()
        {
            return this._token;
        }

        private async Task<string> FetchTokenAsync(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(fetchUri);

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                Debug.WriteLine("Token Uri: {0}", uriBuilder.Uri.AbsoluteUri);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }

}