using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IMediaService
    {
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetByVehicle(string vehicle);
        Task<ApiResponse> GetByReport(string report);
        Task<ApiResponse> Insert(MediaDTO mediaDTO, byte[] fileBytes);
        Task<ApiResponse> Delete(string id);
    }

}
