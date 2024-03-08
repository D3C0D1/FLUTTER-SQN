using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IReportCollection
    {
        Task<Report> GetReportById(string id);
        Task<List<Report>> GetReportsByCustomer(string customer);
        Task<List<Report>> GetReportsByExpert(string expert);
        Task<List<Report>> GetReportsFromDate(DateTime from);
        Task<List<Report>> GetReportsBetweenDates(DateTime from, DateTime to);
        Task<List<Report>> GetAllReports();
        Task InsertReport(Report report);
        Task UpdateReport(Report report);
        Task DeleteReport(string id);
    }
}