using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class ErrorMessageService: IErrorMessageService
    {
        private readonly IErrorMessageCollection _database = new ErrorMessageCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("ErrorMessageService: GetAll");
            try
            {
                List<ErrorMessage> ErrorMsg = await _database.GetAllErrorMessage();
                if (ErrorMsg.Count > 0)
                    return new ApiResponse(ToListDTO(ErrorMsg));
                return new ApiResponse(new ApiError("No Error messages to show", 
                    SQNErrorCode.ErrorMessageNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"ErrorMessageService: GetById: id {id}");
            try
            {
                ErrorMessage errorMsg = await _database.GetErrorMessageById(id);
                if (errorMsg != null)
                    return new ApiResponse(errorMsg.ToDTO());
                return new ApiResponse(new ApiError($"The Error messages {id} doesn't exist",
                    SQNErrorCode.ErrorMessageNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByCode(string code)
        {
            Console.WriteLine($"ErrorMessageService: GetByCode: code {code}");
            try
            {
                ErrorMessage errorMsg = await _database.GetErrorMessageByCode(code);
                if (errorMsg != null)
                    return new ApiResponse(errorMsg.ToDTO());
                return new ApiResponse(new ApiError($"The Error messages {code} doesn't exist",
                    SQNErrorCode.ErrorMessageNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByValue(int value)
        {
            Console.WriteLine($"ErrorMessageService: GetByValue: code {value}");
            try
            {
                ErrorMessage errorMsg = await _database.GetErrorMessageByValue(value);
                if (errorMsg != null)
                    return new ApiResponse(errorMsg.ToDTO());
                return new ApiResponse(new ApiError($"The Error messages {value} doesn't exist",
                    SQNErrorCode.ErrorMessageNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(ErrorMessageDTO errorMsgDTO)
        {
            Console.WriteLine("ErrorMessageService: Insert: ErrorMessageDTO");
            if (errorMsgDTO == null)
                return new ApiResponse(new ApiError("A null objet can be added for Error message", 
                    SQNErrorCode.NullValue));
            ErrorMessage errorMessage = errorMsgDTO.ToModel();
            ApiError validated = errorMessage.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await CodeValidation(errorMessage.Code, errorMessage.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ValueValidation(errorMessage.Value, errorMessage.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            try
            {
                await _database.InsertErrorMessage(errorMessage);
                return new ApiResponse(errorMessage.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(ErrorMessageDTO errorMsgDTO, string id)
        {
            Console.WriteLine($"ErrorMessageService: Update: ErrorMessageDTO");
            if (errorMsgDTO == null)
                return new ApiResponse(new ApiError("A null objet can´t be used for update the Error Message " + id, 
                    SQNErrorCode.NullValue));
            ErrorMessage errorMessage = errorMsgDTO.ToModel();
            ApiError validated = errorMessage.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await CodeValidation(errorMessage.Code, errorMsgDTO.id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ValueValidation(errorMessage.Value, errorMsgDTO.id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            errorMessage.id = new ObjectId(id);
            try
            {
                await _database.UpdateErrorMessage(errorMessage);
                return new ApiResponse(errorMessage.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"ErrorMessageService: Delete: id {id}");
            try
            {
                await _database.DeleteErrorMessage(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private async Task<ApiError> ValueValidation(int value, string id)
        {
            Console.WriteLine($"ErrorMessageService: ValueValidation: value: {value}, id: {id} ");
            try
            {
                ErrorMessage valida = await _database.GetErrorMessageByValue(value);
                if (valida != null)
                    if (valida.id.ToString() != id)
                        return new ApiError("Error Message with value " + value + " exist",
                            SQNErrorCode.ErrorMessageAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private async Task<ApiError> CodeValidation(string code, string id)
        {
            Console.WriteLine($"ErrorMessageService: CodeValidation: code: {code}, id: {id}");
            try
            {
                ErrorMessage valida = await _database.GetErrorMessageByCode(code);
                if (valida != null)
                    if (valida.id.ToString() != id)
                        return new ApiError("Data Element with code " + code +" already exist",
                            SQNErrorCode.DataElementAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private List<ErrorMessageDTO> ToListDTO(List<ErrorMessage> errorMsg)
        {
            List<ErrorMessageDTO> response = new();
            foreach (ErrorMessage em in errorMsg)
                response.Add(em.ToDTO());
            return response;
        }
    }
}