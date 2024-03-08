using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IServiceService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetByCustomer(string customer);
        Task<ApiResponse> GetByExpert(string expert);
        Task<ApiResponse> GetByCustomerAndStatus(string customer, string status);
        Task<ApiResponse> GetByExpertAndStatus(string expert, string status);
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> CreateService(ServiceDTO serviceDTO, string user);
        Task<ApiResponse> Update(ServiceDTO serviceDTO, string id, string user);
        Task<ApiResponse> AddClassifications(List<DataElementDTO> classifications, string id, string user);
        Task<ApiResponse> AddCalification(CalificationDTO calification, string id, string user);
        Task<ApiResponse> AddExpert(string expert, string id, string user);
        Task<ApiResponse> ChangeStatus(string status, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}