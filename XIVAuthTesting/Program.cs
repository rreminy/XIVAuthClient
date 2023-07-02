using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using XIVAuth;
using System.Net.Http.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace XIVAuth.Testing
{
    public static class Program
    {
        public static async Task Main()
        {
            await OldMain(null);
            return;
            using var xivAuthClient = new XIVAuthClient();
            var clientInfo = GetClientInformation();

            using var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:18080/callback/");
            httpListener.Start();

            using var authClient = new AuthenticationApiClient(new Uri("https://edge.xivauth.net"));
            var authUri = authClient.BuildAuthorizationUrl()
                .WithClient(clientInfo.ClientId)
                .WithRedirectUrl("http://localhost:18080/callback")
                .WithScope(string.Join(' ', clientInfo.Scopes))
                .WithNonce(new Random().Next(10000).ToString())
                .Build()
                .ToString()
                .Replace("/authorize", "/oauth/authorize");

            Console.WriteLine($"Opening: {authUri}");
            Process.Start(new ProcessStartInfo(authUri.ToString()) { UseShellExecute = true });

            Console.WriteLine("Waiting callback");
            using var cts = new CancellationTokenSource(60000);
            var context = await httpListener.GetContextAsync().WaitAsync(cts.Token);
            Console.WriteLine($"Received: {context.Request.RawUrl}");

            var authCode = context.Request.QueryString["code"];
            if (authCode is not null)
            {
                Console.WriteLine($"Authorization Code: {authCode}");
            }
            else
            {
                var error = context.Request.QueryString["error"];
                var errorDescription = context.Request.QueryString["error_description"];
                Console.WriteLine($"Error details: {error} ({errorDescription})");
            }
            context.Response.Close(Encoding.UTF8.GetBytes("Received"), true);

            var token = await authClient.GetTokenAsync(new AuthorizationCodeTokenRequest() { ClientId = clientInfo.ClientId, ClientSecret = clientInfo.ClientSecret, Code = authCode, RedirectUri = "http://localhost:18080/callback" });
            Console.WriteLine($"Received token: {token.TokenType} {token.AccessToken}");
            Console.WriteLine($"Refresh token: {token.RefreshToken}");

            using var xivUser = xivAuthClient.GetUser(token.AccessToken);
            var userInfo = await xivUser.User.GetAsync();
            Console.WriteLine(userInfo.Id);
        }
        
        public static async Task OldMain(string[] args)
        {
            using var xivAuthClient = new XIVAuthClient();
            using var httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:18080/callback/");
            httpListener.Start();

            var clientInfo = GetClientInformation();

            Console.WriteLine();

            var authUri = xivAuthClient.Flows.GetCodeAuthorizationUri(clientInfo.ClientId, new("http://localhost:18080/callback"), new Random().Next(10000).ToString(), clientInfo.Scopes.AsEnumerable());

            Console.WriteLine($"Opening {authUri}");
            Process.Start(new ProcessStartInfo(authUri.ToString()) { UseShellExecute = true });

            using var cts = new CancellationTokenSource(60000);

            Console.WriteLine("Waiting callback");
            var context = await httpListener.GetContextAsync().WaitAsync(cts.Token);
            Console.WriteLine($"Received: {context.Request.RawUrl}");

            var authCode = context.Request.QueryString["code"];
            if (authCode is not null)
            {
                Console.WriteLine($"Authorization Code: {authCode}");
            }
            else
            {
                var error = context.Request.QueryString["error"];
                var errorDescription = context.Request.QueryString["error_description"];
                Console.WriteLine($"Error details: {error} ({errorDescription})");
            }
            context.Response.Close(Encoding.UTF8.GetBytes("<html><head><title>XIVAuth Callback Received</title></head><body><h1>XIVAuth Callback Received</h1><p>You may close this window</p><script>close()</script></body></html>"), true);


            var tokenUri = $"{xivAuthClient.Options.OAuthUrl}token";
            using var httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync(tokenUri, new Dictionary<string, string>() { { "grant_type", "authorization_code" }, {"client_id", clientInfo.ClientId }, { "client_secret", clientInfo.ClientSecret }, { "code", authCode }, { "redirect_uri", "http://localhost:18080/callback" }, { "scopes" , string.Join(' ', clientInfo.Scopes) } });
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            response.EnsureSuccessStatusCode();
            var responseJson = JsonSerializer.Deserialize<Dictionary<string, object>>(content)!;

            var xivUser = xivAuthClient.GetUser(responseJson["access_token"].ToString());
            var characters = await xivUser.Characters.GetAllAsync();
            Console.WriteLine(string.Join('\n', characters.Select(ModelUtils.GetDetailedString)));

        }

        public static ClientInformation GetClientInformation()
        {
            const string clientIdEnvVarName = "XIVAUTH_CLIENT_ID";
            const string clientSecretEnvVarName = "XIVAUTH_CLIENT_SECRET";
            const string clientScopesEnvVarName = "XIVAUTH_CLIENT_SCOPES";

            var clientId = Environment.GetEnvironmentVariable(clientIdEnvVarName);
            var clientSecret = Environment.GetEnvironmentVariable(clientSecretEnvVarName);
            var clientScopes = Environment.GetEnvironmentVariable(clientScopesEnvVarName);

            if (clientId is null)
            {
                Console.Write("Client ID: ");
                clientId = Console.ReadLine() ?? throw new ArgumentException($"Unable to read {clientIdEnvVarName}");
            }
            if (clientSecret is null)
            {
                Console.Write("Client Secret: ");
                clientSecret = Console.ReadLine() ?? throw new ArgumentException($"Unable to read {clientSecretEnvVarName}");
            }
            if (clientScopes is null)
            {
                Console.Write("Client Scopes: ");
                clientScopes = Console.ReadLine() ?? throw new ArgumentException($"Unable to read {clientScopesEnvVarName}");
            }

            return new() { ClientId = clientId, ClientSecret = clientSecret, Scopes = clientScopes.Split(" ", StringSplitOptions.RemoveEmptyEntries) };
        }
    }
}
