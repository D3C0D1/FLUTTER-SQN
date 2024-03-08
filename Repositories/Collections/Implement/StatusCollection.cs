using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class StatusCollection : IStatusCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Status> _statuses;

        public StatusCollection()
        {
            _statuses = _repository.database.GetCollection<Status>("Status");
        }

        public async Task DeleteStatus(string id)
        {
            var filter = Builders<Status>.Filter.Eq(s => s.id, new ObjectId(id));
            await _statuses.DeleteOneAsync(filter);
        }

        public async Task<List<Status>> GetAllStatus()
        {
            return await _statuses.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Status> GetStatusById(string id)
        {
            return await _statuses.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Status> GetStatusByName(string name)
        {
            return await _statuses.FindAsync(
                new BsonDocument { { "Name", name } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Status> GetStatusByCode(string code)
        {
            return await _statuses.FindAsync(
                new BsonDocument { { "Code", code } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertStatus(Status status)
        {
            await _statuses.InsertOneAsync(status);
        }

        public async Task Updatestatus(Status status)
        {
            FilterDefinition<Status> filter = Builders<Status>.Filter.Eq(s => s.id, status.id);
            await _statuses.ReplaceOneAsync(filter, status);
        }

        public async Task<Status> IsForCreation()
        {
            return await _statuses.FindAsync(
                new BsonDocument { { "Creation", new BsonBoolean(true) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<Status>> ListByValid(bool valid)
        {
            return await _statuses.FindAsync(
                new BsonDocument { { "Valid", new BsonBoolean(valid) } }).Result.ToListAsync();
        }
    }
}