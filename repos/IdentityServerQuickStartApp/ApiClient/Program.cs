using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string identityServerHost = @"https://localhost:5001";
            const string clientId = "webapi_client";
            const string clientIdWithPasswordGrant = "ro.client";
            const string userName = "John Wayne";
            const string userPassword = "good_dead_indsman";
            const string secret = "secret";
            const string api = "api_one";
            const string apiEndPoint = @"https://localhost:5003/identity";

            var discoveryResponse = GetDiscoveryResponse(identityServerHost).GetAwaiter().GetResult();
            
            if (discoveryResponse != null)
            {
                //var tokenResponse = GetTokenResponse(discoveryResponse, api, clientId, secret).GetAwaiter().GetResult();
                var tokenResponse = GetTokenResponseWithClientPasswordGrant(
                    discoveryResponse, 
                    api,
                    clientIdWithPasswordGrant, 
                    secret, 
                    userName, 
                    userPassword)
                    .GetAwaiter()
                    .GetResult();

                if (tokenResponse != null)
                {
                    Console.WriteLine(tokenResponse.Json);
                    var accessToken = tokenResponse.AccessToken;
                    CallApi(accessToken, apiEndPoint).GetAwaiter().GetResult();
                }
            }

            Console.ReadLine();
        }

        private static async Task<DiscoveryResponse> GetDiscoveryResponse(string host)
        {
            var discoveryResponse = await DiscoveryClient.GetAsync(host);

            if (discoveryResponse.IsError)
            {
                Console.WriteLine(discoveryResponse.Error);
                return null;
            }

            return discoveryResponse;
        }

        private static async Task<TokenResponse> GetTokenResponse(DiscoveryResponse response, string api, string clientId, string secret)
        {
            var tokenClient = new TokenClient(response.TokenEndpoint, clientId, secret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(api);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            return tokenResponse;
        }

        private static async Task<TokenResponse> GetTokenResponseWithClientPasswordGrant(
            DiscoveryResponse response,
            string api, 
            string clientId, 
            string secret, 
            string user, 
            string password)
        {
            var tokenClient = new TokenClient(response.TokenEndpoint, clientId, secret);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(user, password, api);
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            return tokenResponse;
        }

        private static async Task CallApi(string accessToken, string endPoint)
        {
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync(endPoint);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
