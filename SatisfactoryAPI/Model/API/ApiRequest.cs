namespace SatisfactoryAPI.Model;

public class ApiRequest
{
    public string Function { get; set; } = string.Empty;
    public object? Data { get; set; }
}