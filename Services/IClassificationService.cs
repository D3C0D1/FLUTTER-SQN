using SQNBack.Models.DTO;
using SQNBack.Utils;

namespace SQNBack.Services
{
    public interface IClassificationService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> Insert(ClassificationDTO classificationDTO, string user);
        Task<ApiResponse> Update(ClassificationDTO classificationDTO, string id, string user);
        Task<ApiResponse> validateClassifications(List<DataElementDTO> classifications);
        Task<ApiResponse> Delete(string id);
    }
}