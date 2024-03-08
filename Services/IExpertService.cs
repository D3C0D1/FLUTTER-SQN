using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IExpertService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetById(string docTye, string docNumber);
        Task<ApiResponse> GetByCellPhone(string cellPhone);
        Task<ApiResponse> GetByEmail(string email);
        Task<ApiResponse> NotifyExperts(UbicationDTO ubication);
        Task<ApiResponse> Insert(ExpertDTO expertDTO, string user);
        Task<ApiResponse> Update(ExpertDTO expertDTO, string user, string id);
        Task<ApiResponse> AddUbication(UbicationDTO ubication, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}