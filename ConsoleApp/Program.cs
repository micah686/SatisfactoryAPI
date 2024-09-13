using SatisfactoryAPI;
using SatisfactoryAPI.Model;
using System.Diagnostics;

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
            var healthStatus = await ApiFunctions.HealthCheck(apiClient, new SatisfactoryAPI.Model.Endpoints.HealthCheck.HealthCheckPayload());
            Console.WriteLine($"Server health: {healthStatus.Health}");
            Console.WriteLine($"Server custom data: {healthStatus.ServerCustomData}");

            // Authenticate
            var authToken = await ApiFunctions.PasswordLogin(apiClient, PrivilegeLevel.Administrator, adminPassword);
            Console.WriteLine($"Authentication successful. Token: {authToken}");
           

            // Set the authentication token for future requests
            apiClient.SetAuthToken(authToken);

            //======Any main logic below here====
            Debug.WriteLine("Now doing main functions");

            var saveName = "API_NEW_SAVE";


            




            //var data2 = await apiClient.DownloadSave(data);



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