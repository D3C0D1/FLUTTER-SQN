using SQNBack.Models;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public interface IReportService
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> GetByCustomer(string customer);
        Task<ApiResponse> GetByExpert(string expert);
        Task<ApiResponse> GetById(string id);
        Task<ApiResponse> GetFromDate(string from);
        Task<ApiResponse> GetBetweenDates(string from, string to);
        Task<ApiResponse> Insert(Report report, string user);
        Task<ApiResponse> Update(Report report, string id, string user);
        Task<ApiResponse> Delete(string id);
    }
}