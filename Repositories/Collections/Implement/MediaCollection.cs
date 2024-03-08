using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class MediaCollection : IMediaCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Media> _medias;

        public MediaCollection()
        {
            _medias = _repository.database.GetCollection<Media>("Media");
        }

        public async Task<Media> GetMediaById(string id)
        {
            return await _medias.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<Media>> GetMediaByReport(string report)
        {
            return await _medias.FindAsync(
                new BsonDocument { { "Report", report } }).Result.ToListAsync();
        }

        public async Task<List<Media>> GetMediaByVehicle(string vehicle)
        {
            return await _medias.FindAsync(
                new BsonDocument { { "Vehicle", vehicle } }).Result.ToListAsync();
        }

        public async Task InsertOne(Media media)
        {
            await _medias.InsertOneAsync(media);
        }

        public async Task DeleteMedia(string id)
        {
            var filter = Builders<Media>.Filter.Eq(r => r.id, new ObjectId(id));
            await _medias.DeleteOneAsync(filter);
        }

        public async Task InsertMedia(List<Media> media)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Media>> GetMediaByReport()
        {
            throw new NotImplementedException();
        }
    }
}