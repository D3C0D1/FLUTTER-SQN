using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class DateElementCollection : IDataElementCollection
    {
        private readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<DataElement> _dataElement;

        public DateElementCollection()
        {
            _dataElement = _repository.database.GetCollection<DataElement>("DataElement");
        }

        public async Task DeleteDocumentElement(string id)
        {
            var filter = Builders<DataElement>.Filter.Eq(dt => dt.Id, new ObjectId(id));
            await _dataElement.DeleteOneAsync(filter);
        }

        public async Task<List<DataElement>> GetAllDocumentElements()
        {
            return await _dataElement.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<DataElement> GetDocumentElementById(string id)
        {
            return await _dataElement.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<DataElement>> GetDocumentElementsByGroup(string group)
        {
            return await _dataElement.FindAsync(
                new BsonDocument { { "Group", group } }).Result.ToListAsync();
        }

        public async Task<DataElement> GetDocumentElementsByNameAndGroup(string name, string group)
        {
            var filter = Builders<DataElement>.Filter.And( Builders<DataElement>.Filter.Eq("Name", name),
                                                           Builders<DataElement>.Filter.Eq("Group", group));
            return await _dataElement.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task InsertDocumentElement(DataElement dataElement)
        {
            await _dataElement.InsertOneAsync(dataElement);
        }

        public async Task UpdateDocumentElement(DataElement dataElement)
        {            
            FilterDefinition<DataElement> filter = Builders<DataElement>.Filter.Eq(de => de.Id, dataElement.Id);
            await _dataElement.ReplaceOneAsync(filter, dataElement);
            //ObjectUpdateDefinition<DataElement> updt = new(dataElement);
            //await _dataElement.UpdateOneAsync(filter, updt);
        }
    }
}