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

    

    

    public async Task<T> SendRequest<T>(ApiCallName function, object? data)
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
    
    
    
    //==============Download File======================
    private async Task<HttpResponseMessage> SendFileRequest(ApiCallName function, object data)
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
            //HandleApiError(errorResponse);
        }

        // Check if the response is a file
        if (response.Content.Headers.ContentType.MediaType != "application/octet-stream")
        {
            //throw new SatisfactoryApiException("unexpected_response", "Expected a file response but received a different content type.");
        }
        return response;
    }
    
    private async Task<Stream> DownloadSaveGame(string saveName)
    {
        var request = new DownloadSavePayload { SaveName = saveName };
        
        // We're using a custom method here instead of SendRequest because
        // the response is a file stream, not a JSON object
        var response = await SendFileRequest(ApiCallName.DownloadSaveGame, request);
        
        return await response.Content.ReadAsStreamAsync();
    }
    
    public async Task SaveDownloadedSaveGame(string saveName, string localFilePath)
    {
        using (var saveGameStream = await DownloadSaveGame(saveName))
        using (var fileStream = File.Create(localFilePath))
        {
            await saveGameStream.CopyToAsync(fileStream);
        }
    }

    
}