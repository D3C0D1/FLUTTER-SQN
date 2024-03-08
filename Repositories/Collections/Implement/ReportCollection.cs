using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class ReportCollection : IReportCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Report> _reports;

        public ReportCollection()
        {
            _reports = _repository.database.GetCollection<Report>("Report");
        }

        public async Task DeleteReport(string id)
        {
            var filter = Builders<Report>.Filter.Eq(r => r.id, new ObjectId(id));
            await _reports.DeleteOneAsync(filter);
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _reports.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<List<Report>> GetReportsByCustomer(string customer)
        {
            return await _reports.FindAsync(
                new BsonDocument { { "Customer", customer } }).Result.ToListAsync();
        }

        public async Task<List<Report>> GetReportsByExpert(string expert)
        {
            return await _reports.FindAsync(
                new BsonDocument { { "Expert", expert } }).Result.ToListAsync();
        }

        public async Task<Report> GetReportById(string id)
        {
            return await _reports.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<Report>> GetReportsFromDate(DateTime from)
        {
            return await _reports.FindAsync(
                new BsonDocument { { "CreationDate", from } }).Result.ToListAsync();
        }
        
        public async Task<List<Report>> GetReportsBetweenDates(DateTime from, DateTime to)
        {
            return await _reports.FindAsync(
                new BsonDocument { { "CreationDate", from }, { "CreationDate", to } }).Result.ToListAsync();
        }

        public async Task InsertReport(Report report)
        {
            await _reports.InsertOneAsync(report);
        }

        public async Task UpdateReport(Report report)
        {
            FilterDefinition<Report> filter = Builders<Report>.Filter.Eq(r => r.id, report.id);
            await _reports.UpdateOneAsync(filter, new ObjectUpdateDefinition<Report>(report));
        }

        Task<List<Report>> IReportCollection.GetReportsFromDate(DateTime from)
        {
            throw new NotImplementedException();
        }

        Task<List<Report>> IReportCollection.GetReportsBetweenDates(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}