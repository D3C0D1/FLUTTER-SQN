using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IErrorMessageService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetByCode(string code);
        Task<ApiResponse> GetByValue(int value);
        Task<ApiResponse> Insert(ErrorMessageDTO errorMsgDTO);
        Task<ApiResponse> Update(ErrorMessageDTO errorMsgDTO, string id);
        Task<ApiResponse> Delete(string id);
    }
}