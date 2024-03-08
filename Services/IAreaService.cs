using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IAreaService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetByName(string name);
        Task<ApiResponse> GetByNameAndOrder(string area, int order);
        Task<ApiResponse> Insert(AreaDTO areaDTO, string user);
        Task<ApiResponse> InsertRange(RangeDTO range, string user);
        Task<ApiResponse> Update(AreaDTO areaDTO, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}