using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SatisfactoryAPI.Model;
using SatisfactoryAPI.Model.Endpoints.DownloadSave;
using SatisfactoryAPI.Model.Enums;

namespace SatisfactoryAPI;

public class DedicatedServerApiClient
{
    private readonly HttpClient _httpClient;
    private string _authToken;

    public DedicatedServerApiClient(string baseUrl, bool acceptInvalidCertificate = false)
    {
        var handler = new HttpClientHandler();
        if (acceptInvalidCertificate)
        {
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        }

        _httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
    }

    public void SetAuthToken(string token)
    {
        _authToken = token;
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    

    

    internal async Task<T> SendRequest<T>(ApiCallName function, object? data)
    {
        var request = new ApiRequest { Function = function.ToString(), Data = data };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("", content);
        response.EnsureSuccessStatusCode();
        if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return default;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (apiResponse.ErrorCode != null)
        {
            throw new Exception($"API Error: {apiResponse.ErrorCode} - {apiResponse.ErrorMessage}");
        }

        return apiResponse.Data;
    }
    
    internal async Task<HttpResponseMessage> DownloadSave(ApiCallName function, object data)
    {
        var request = new ApiRequest { Function = function.ToString(), Data = data };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("", content);
        
        // Check for errors
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            HandleApiError.HandleError(errorResponse);
        }

        // Check if the response is a file
        if (response.Content.Headers.ContentType.MediaType != "application/octet-stream")
        {
            throw new ApiException("unexpected_response", "Expected a file response but received a different content type.");
        }
        return response;
    }

    internal async Task UploadSaveFile(ApiCallName function, object data, Stream fileStream, string fileName)
    {
        using (var content = new MultipartFormDataContent())
        {
            // Add the JSON part
            var apiRequest = new ApiRequest { Function = function.ToString(), Data = data };
            var jsonContent = new StringContent(JsonSerializer.Serialize(apiRequest), System.Text.Encoding.UTF8, "application/json");
            content.Add(jsonContent, "data");

            // Add the file part
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, "saveGameFile", fileName);

            // Send the request
            var response = await _httpClient.PostAsync("", content);

            // Check for errors
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                HandleApiError.HandleError(errorResponse);
            }
            
            Console.WriteLine($"Response Status: {response.StatusCode}");
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");

            // Handle specific status codes
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.Created:
                case System.Net.HttpStatusCode.Accepted:
                case System.Net.HttpStatusCode.NoContent:
                    // These are all considered successful outcomes
                    break;
                default:
                    throw new ApiException("unexpected_response", $"Unexpected response status: {response.StatusCode}");
            }
        }
    }
}