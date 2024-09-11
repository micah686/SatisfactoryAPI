using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SatisfactoryAPI.POCO;

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

    public async Task<HealthCheckResponse> HealthCheck()
    {
        return await SendRequest<HealthCheckResponse>("HealthCheck", new { ClientCustomData = "" });
    }

    public async Task<string> PasswordLogin(string privilegeLevel, string password)
    {
        var response = await SendRequest<AuthResponse>("PasswordLogin", new { MinimumPrivilegeLevel = privilegeLevel, Password = password });
        return response.AuthenticationToken;
    }

    private async Task<T> SendRequest<T>(string function, object data)
    {
        var request = new ApiRequest { Function = function, Data = data };
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (apiResponse.ErrorCode != null)
        {
            throw new Exception($"API Error: {apiResponse.ErrorCode} - {apiResponse.ErrorMessage}");
        }

        return apiResponse.Data;
    }
}