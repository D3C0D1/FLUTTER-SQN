using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IStatusService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> Insert(StatusDTO statusDTO, string user);
        Task<ApiResponse> Update(StatusDTO statusDTO, string id, string user);
        Task<ApiResponse> Delete(string id);
        Task<ApiResponse> IsForCreation(string id);
        Task<ApiResponse> IsForCreation();
        Task<ApiResponse> IsValid(string id);
        Task<ApiResponse> ListByValid(bool valid);
    }
}