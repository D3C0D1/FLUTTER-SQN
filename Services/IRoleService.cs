using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IRoleService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetByName(string name);
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> Insert(RoleDTO role, string user);
        Task<ApiResponse> Update(RoleDTO role, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}