using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IDataElementService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetByGroup(string code);
        Task<ApiResponse> Insert(DataElementDTO documentTypeDTO, string user);
        Task<ApiResponse> Update(DataElementDTO documnetDTO, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}