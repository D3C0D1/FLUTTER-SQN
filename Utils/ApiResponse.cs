namespace SQNBack.Utils
{

    [Serializable]
    public class ApiResponse
    {
        public bool Success { get; set; }
        public dynamic Result { get; set; }
        public ApiError Error { get; set; }

        public ApiResponse(dynamic result)
        {
            Success = true;
            Result = result;
            Error = new ApiError();
        }

        public ApiResponse(Exception ex)
        {
            Success = false;
            Result = ex.ToString();
            Error = new ApiError(ex.Message, SQNErrorCode.SystemError);
        }

        public ApiResponse(ApiError error)
        {
            Success = false;
            Result = string.Empty;
            Error = error;
        }

    }
}