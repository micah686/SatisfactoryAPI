namespace SatisfactoryAPI.Model;

public class ApiException: Exception
{
    public string ErrorCode { get; }
    public object ErrorData { get; }

    public ApiException(string errorCode, string message, object errorData = null) 
        : base(message)
    {
        ErrorCode = errorCode;
        ErrorData = errorData;
    }
}