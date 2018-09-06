using System;
using System.Linq;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ConsoleClientApp
{
    class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] arguments)
        {
            const string authorityEndPoint = @"https://localhost:5001";
            const string consoleClientId = "client";
            const string consoleClientSecret = "secret";
            const string apiName = "api_one";
            const string apiEndPoint = @"https://localhost:5005/api/identity";

            var tokenEndPoint = await DisccoverClientAsync(authorityEndPoint);
            var accessToken = await GetAccessTokenAsync(tokenEndPoint, consoleClientId, consoleClientSecret, apiName);
            var jsonContent = await GetContentFromApiAsync(accessToken, apiEndPoint);

            Console.WriteLine(jsonContent);
            Console.ReadKey();
        }

        private static async Task<string> DisccoverClientAsync(string endPoint)
        {
            if (!CheckStringParametersIsNotNullOrEmpty(new [] { endPoint }))
            {
                return String.Empty;
            }

            var discoveryResponse = await DiscoveryClient.GetAsync(endPoint);

            if (discoveryResponse.IsError)
            {
                Console.WriteLine(discoveryResponse.Error);
                return String.Empty;
            }

            var tokenEndPoint = discoveryResponse.TokenEndpoint;
            return tokenEndPoint;
        }

        private static async Task<string> GetAccessTokenAsync(string tokenEndPoint, string clientId, string clientSecret, string apiName)
        {
            if (!CheckStringParametersIsNotNullOrEmpty(new[] { tokenEndPoint, clientId, clientSecret, apiName }))
            {
                return String.Empty;
            }

            var tokenClient = new TokenClient(tokenEndPoint, clientId, clientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(apiName);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return String.Empty;
            }

            return tokenResponse.AccessToken;
        }

        private static async Task<string> GetContentFromApiAsync(string accessToken, string apiHost)
        {
            if (!CheckStringParametersIsNotNullOrEmpty(new[] { accessToken, apiHost }))
            {
                return String.Empty;
            }

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(accessToken);

            var apiResponse = await httpClient.GetAsync(apiHost);
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.StatusCode);
                return String.Empty;
            }
            
            var content = await apiResponse.Content.ReadAsStringAsync();
            var jsonContent = JArray.Parse(content).ToString();

            return jsonContent;
        }

        private static bool CheckStringParametersIsNotNullOrEmpty(string[] parameters)
        {
            if (parameters == null)
            {
                return false;
            }

            return parameters.All(p => !string.IsNullOrEmpty(p));
        }
    }
}
