using SatisfactoryAPI.Model;
namespace SatisfactoryAPI;

public class HandleApiError
{
    public static void HandleError(ApiResponse<object> response)
    {
        switch (response.ErrorCode)
        {
            case "save_game_load_failed":
                throw new ApiException(response.ErrorCode, "Failed to find or load the specified save game file.");
            case "invalid_save_game":
                throw new ApiException(response.ErrorCode, "The save game file is invalid or corrupted.");
            case "unsupported_save_game":
                throw new ApiException(response.ErrorCode, "The save game file version is not supported.");
            case "file_save_failed":
                throw new ApiException(response.ErrorCode, "Failed to save the game file to the server.");
            case "file_not_found":
                throw new ApiException(response.ErrorCode, "The specified save game file was not found on the server.");
            default:
                throw new ApiException(response.ErrorCode, response.ErrorMessage, response.Data);
        }
    }
}