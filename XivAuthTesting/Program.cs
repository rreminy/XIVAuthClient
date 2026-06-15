using System.Diagnostics;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;

namespace XivAuth.Testing
{
    public static class Program
    {
        public const string RedirectUrl = "http://localhost:18080/callback/";
        public static async Task Main()
        {
            // Step 0: Gather information
            using var client = new XivAuthClient();
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
            var context = await httpListener.GetContextAsync().WaitAsync(TimeSpan.FromSeconds(300));
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
            using var clientUser = client.GetUser(token.AccessToken);
            var user = await clientUser.User.GetAsync();
            var userInfo = ModelUtils.GetDetailedString(user);
            var userJwt = await clientUser.User.GetJwtAsync();
            Console.WriteLine($"{userInfo}\n{userJwt}\n");

            // Step 7: Get all characters information
            var characters = await clientUser.Characters.GetAllAsync();
            foreach (var character in characters)
            {
                var characterInfo = ModelUtils.GetDetailedString(character);
                var characterJwt = await clientUser.Characters.GetJwtAsync(character.LodestoneId);
                Console.WriteLine($"{characterInfo}\n{characterJwt}\n");
            }
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
