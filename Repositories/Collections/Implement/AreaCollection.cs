using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class AreaCollection : IAreaCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Area> _areas;

        public AreaCollection()
        {
            _areas = _repository.database.GetCollection<Area>("Area");
        }

        public async Task DeleteArea(string id)
        {
            var filter = Builders<Area>.Filter.Eq(a => a.id, new ObjectId(id));
            await _areas.DeleteOneAsync(filter);
        }

        public async Task<List<Area>> GetAllAreas()
        {
            return await _areas.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Area> GetAreaById(string id)
        {
            return await _areas.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Area> GetAreaByName(string name)
        {
            return await _areas.FindAsync(
                new BsonDocument { { "Name", name } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertArea(Area area)
        {
            await _areas.InsertOneAsync(area);
        }

        public async Task UpdateArea(Area area)
        {
            FilterDefinition<Area> filter = Builders<Area>.Filter.Eq(a => a.id, area.id);
            await _areas.ReplaceOneAsync(filter, area);
        }
    }
}