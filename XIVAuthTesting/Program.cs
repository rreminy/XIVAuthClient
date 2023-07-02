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
using System.Runtime.CompilerServices;

namespace XIVAuth.Testing
{
    public static class Program
    {
        public const string RedirectUrl = "http://localhost:18080/callback/";
        public static async Task Main()
        {
            // Step 0: Gather information
            using var client = new XIVAuthClient();
            var clientInfo = GetClientInformation();

            // Step 1: Start HTTP Listener server
            Console.WriteLine("Starting HTTP Listener");
            using var httpListener = new HttpListener();
            httpListener.Prefixes.Add(RedirectUrl);
            httpListener.Start();

            // Step 2: Send user to web authorization flow
            var state = Guid.NewGuid().ToString();
            var authUri = client.Flows.GetCodeAuthorizationUri(clientInfo.ClientId, new(RedirectUrl), state, clientInfo.Scopes);
            Console.WriteLine($"Opening: {authUri}");
            Process.Start(new ProcessStartInfo(authUri.ToString()) { UseShellExecute = true });

            // Step 3: Wait for response via a callback URL
            Console.WriteLine($"Waiting for callback at {RedirectUrl}");
            var context = await httpListener.GetContextAsync().WaitAsync(TimeSpan.FromSeconds(90));
            Console.WriteLine($"Callback received: {context.Request.RawUrl}\n");

            // Step 4: Validate state and get Authorization Code
            var state2 = context.Request.QueryString["state"];
            if (state != state2)
            {
                Console.WriteLine("state doesn't match");
                Console.WriteLine($"{state}\n{state2}");
                return;
            }

            var authCode = context.Request.QueryString["code"];
            if (authCode is null)
            {
                var error = context.Request.QueryString["error"];
                var errorDescription = context.Request.QueryString["error_description"];
                Console.WriteLine($"Error details: {error} ({errorDescription})");
                return;
            }
            Console.WriteLine($"Authorization Code: {authCode}");
            context.Response.Close(Encoding.UTF8.GetBytes("<html><head><title>XIVAuth Callback Received</title></head><body><h1>XIVAuth Callback Received</h1><p>You may close this window</p></body></html>"), true);

            // Step 5: Get bearer token
            var token = await client.Flows.GetTokenAsync(clientInfo.ClientId, clientInfo.ClientSecret, authCode, new(RedirectUrl));
            Console.WriteLine($"Bearer Token: {token.AccessToken}\n");
            
            // Step 6: Get XIVAuth User and get its information
            using var user = client.GetUser(token.AccessToken);
            var userInfo = await user.User.GetAsync();
            Console.WriteLine($"{userInfo}\n");

            // Step 7: Get all characters information
            var characters = await user.Characters.GetAllAsync();
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
