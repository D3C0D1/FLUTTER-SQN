using SQNBack.Models.DTO;
using SQNBack.Utils;


namespace SQNBack.Services
{
    public interface IInvolvedService
    {
        Task<ApiResponse> GetAllByReport(string report);
        Task<ApiResponse> GetAllByPlateID(string PlateNumber);
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> Insert(InvolvedDTO formInvolvedDTO, string PlateNumber, int IdenficationNumber);
        Task<ApiResponse> Update(InvolvedDTO formInvolvedDTO, string id, string PlateNumber, 
            int IdentificationNumber, string report);
        Task<ApiError> InvolvedIdValidation(string id, string report);
        Task<ApiError> InvolvedReportValidation(string report, string id);
        Task<ApiError> InvolvedPlateNumberValidation(string PlateNumber, string id);
        Task<ApiResponse> Delete(string id);        
    }
}
