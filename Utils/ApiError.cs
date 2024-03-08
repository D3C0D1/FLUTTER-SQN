namespace SQNBack.Utils
{
    public class ApiError
    {
        public string Message { get; set; }
        public SQNErrorCode Code { get; set; }

        public ApiError()
        {
            Message = string.Empty;
            Code = SQNErrorCode.None;
        }

        public ApiError(Exception ex)
        {
            Message = ex.Message;
            Code = SQNErrorCode.SystemError;
        }

        public ApiError(string message, SQNErrorCode code)
        {
            Message = message;
            Code = code;
        }
    }
}