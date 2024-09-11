using SatisfactoryAPI;
using System.Dynamic;
using System.Text.Json;
using SatisfactoryAPI.Model;
using System.Text.Json.Serialization;
using System.Diagnostics;
using SatisfactoryAPI.Model.DataPayloads;

namespace ConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string baseUrl = "https://localhost:7777/api/v1/"; // Replace with your server's address
        string adminPassword = File.ReadAllText("password.txt"); // Replace with your admin password

        var apiClient = new DedicatedServerApiClient(baseUrl, true);

        try
        {
            // Perform health check
            var healthStatus = await apiClient.HealthCheck();
            Console.WriteLine($"Server health: {healthStatus.Health}");
            Console.WriteLine($"Server custom data: {healthStatus.ServerCustomData}");

            // Authenticate
            var authToken = await apiClient.PasswordLogin(PrivilegeLevel.Administrator, adminPassword);
            Console.WriteLine($"Authentication successful. Token: {authToken}");

            // Set the authentication token for future requests
            apiClient.SetAuthToken(authToken);

            //var data = await apiClient.SendRequest<QueryServerState>("QueryServerState", null);
            //var objData = await apiClient.SendRequest<GetServerOptions>("GetServerOptions", null);
            //var objData = await apiClient.SendRequest<RespAdvancedGameSettings>("GetAdvancedGameSettings", null);

            var renameRequest = new DataRenameServer
            {
                ServerName = "LocalDedicatedServer"
            };


            var result = await apiClient.SendRequest<ExpandoObject>("RenameServer", renameRequest);
            //var objData = await apiClient.SendRequest<ExpandoObject>("funcNameHere", null);

            //var json = JsonSerializer.Serialize(objData);

            // You can now use apiClient for other authenticated requests
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}