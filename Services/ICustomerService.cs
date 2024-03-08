using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface ICustomerService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetByCellPhone(string cellPhone);
        Task<ApiResponse> GetByEmail(string email);
        Task<ApiResponse> Insert(CustomerDTO customerDTO, string user);
        Task<ApiResponse> Update(CustomerDTO customerDTO, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}