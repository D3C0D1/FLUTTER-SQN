using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class ClassificationCollection : IClassificationCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Classification> _classifications;

        public ClassificationCollection()
        {
            _classifications = _repository.database.GetCollection<Classification>("Classification");
        }
        public async Task DeleteClassification(string id)
        {
            var filter = Builders<Classification>.Filter.Eq(c => c.id, new ObjectId(id));
            await _classifications.DeleteOneAsync(filter);
        }

        public async Task<List<Classification>> GetAllClassifications()
        {
            return await _classifications.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Classification> GetClassificationById(string id)
        {
            return await _classifications.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Classification> GetClassificationByName(string name)
        {
            return await _classifications.FindAsync(
                new BsonDocument { { "Name", name } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertClassification(Classification classification)
        {
            await _classifications.InsertOneAsync(classification);
        }

        public async Task UpdateClassification(Classification classification)
        {
            FilterDefinition<Classification> filter = Builders<Classification>.Filter.Eq(c => c.id, classification.id);
            await _classifications.UpdateOneAsync(filter, new ObjectUpdateDefinition<Classification>(classification));
        }
    }
}