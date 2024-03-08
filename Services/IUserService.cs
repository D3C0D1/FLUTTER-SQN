using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IUserService
    {
        Task<ApiResponse> GetByRole(string role);
        Task<ApiResponse> GetByPerson(string person);
        Task<ApiResponse> GetByPersonAndRole(string person, string role);
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetByUserName(string userName);
        Task<ApiResponse> Insert(UserDTO userDTO, string creator);
        Task<ApiResponse> Update(UserDTO userDTO, string id, string editor);
        Task<ApiResponse> Delete(string id);        
    }
}