using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class ExpertCollection : IExpertCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private  readonly IMongoCollection<Expert> _experts;

        public ExpertCollection()
        {
            _experts = _repository.database.GetCollection<Expert>("Expert");
        }

        public async Task DeleteExpert(string id)
        {
            var filter = Builders<Expert>.Filter.Eq(e => e.id, new ObjectId(id));
            await _experts.DeleteOneAsync(filter);
        }

        public async Task<List<Expert>> GetAllExperts()
        {
            return await _experts.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Expert> GetExpertByCellPhone(string cellPhone)
        {
            return await _experts.FindAsync(
                new BsonDocument { { "CellPhone", cellPhone } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Expert> GetExpertByEmail(string email)
        {
            return await _experts.FindAsync(
                new BsonDocument { { "Email", email } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Expert> GetExpertById(string id)
        {
            return await _experts.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Expert> GetExpertById(string docTye, string docNumber)
        {
            var filter = Builders<Expert>.Filter.And(Builders<Expert>.Filter.Eq("DocType", docTye),
                                                           Builders<Expert>.Filter.Eq("DocNumber", docNumber));
            return await _experts.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<Expert> GetExpertByProfCard(string profCard)
        {
            return await _experts.FindAsync(
                new BsonDocument { { "ProfCard", profCard } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertExpert(Expert expert)
        {
            await _experts.InsertOneAsync(expert);
        }

        public async Task UpdateExpert(Expert expert)
        {
            FilterDefinition<Expert> filter = Builders<Expert>.Filter.Eq(e => e.id, expert.id);
            await _experts.UpdateOneAsync(filter, new ObjectUpdateDefinition<Expert>(expert));
        }
    }
}