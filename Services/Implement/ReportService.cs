using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class ReportService : IReportService
    {
        private readonly IReportCollection _database = new ReportCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("ReportService: GetAll");
            try
            {
                List<Report> reports = await _database.GetAllReports();
                if (reports.Count > 0)
                    return new ApiResponse(reports);
                return new ApiResponse(new ApiError("No Reports to show", SQNErrorCode.ReportsNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByCustomer(string customer)
        {
            Console.WriteLine($"ReportService: GetByCustomer: customer: {customer}");
            try
            {
                List<Report> reports = await _database.GetReportsByCustomer(customer);
                if (reports != null)
                    return new ApiResponse(reports);
                else
                    return new ApiResponse(new ApiError($"We haven't Reports for the Customer {customer}",
                        SQNErrorCode.ReportsNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByExpert(string expert)
        {
            Console.WriteLine($"ReportService: GetByExpert: expert: {expert}");
            try
            {
                List<Report> reports = await _database.GetReportsByExpert(expert);
                if (reports != null)
                    return new ApiResponse(reports);
                else
                    return new ApiResponse(new ApiError($"The Expert {expert} hasn't reports",
                        SQNErrorCode.ReportsNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"ReportService: GetById: id: {id}");
            try
            {
                Report report = await _database.GetReportById(id);
                if (report != null)
                    return new ApiResponse(report);
                else
                    return new ApiResponse(new ApiError($"The Report {id} doesn't exist",
                        SQNErrorCode.ReportsNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetFromDate(string from)
        {
            Console.WriteLine($"ReportService: GetFromDate: from: {from}");
            try
            {
                List<Report> report = await _database.GetReportsFromDate(DateTime.Parse(from));
                if (report.Count > 0)
                    return new ApiResponse(report);
                else
                    return new ApiResponse(new ApiError($"Doesn't exist reports from {from}",
                        SQNErrorCode.ReportsNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetBetweenDates(string from, string to)
        {
            Console.WriteLine($"ReportService: GetBetWeenDates: from: {from}, to: {to}");
            try
            {
                List<Report> reports = await _database.GetReportsBetweenDates(DateTime.Parse(from), DateTime.Parse(to));
                if (reports.Count > 0)
                    return new ApiResponse(reports);
                else
                    return new ApiResponse(new ApiError($"Doesn't exist Reports between {from} to {to}",
                        SQNErrorCode.ReportsNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(Report report, string user)
        {
            Console.WriteLine("ReportService: Insert: ReportDTO");
            if (report == null)
                return new ApiResponse(new ApiError("A null objet can´t be added for Report",
                    SQNErrorCode.NullValue));
            ApiError validated = report.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            report.Creator = user;
            report.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertReport(report);
                return new ApiResponse(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
            
        }

        public async Task<ApiResponse> Update(Report report, string id, string user)
        {
            Console.WriteLine($"ReportService: Update: ReportDTO, id: {id}");
            if (report == null)
                return new ApiResponse(new ApiError($"A null objet can't be used for update the Report {id}",
                    SQNErrorCode.NullValue));
            ApiError validated = report.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            report.id = new ObjectId(id);
            report.Updater = user;
            report.UpdateDate = DateTime.Now;
            try
            {
                await _database.UpdateReport(report);
                return new ApiResponse(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"ReportService: Delete: id: {id}");
            try
            {
                await _database.DeleteReport(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }
    }
}