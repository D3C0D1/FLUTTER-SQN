using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class ErrorMessageCollection : IErrorMessageCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<ErrorMessage> _errorMsg;

        public ErrorMessageCollection()
        {
            _errorMsg = _repository.database.GetCollection<ErrorMessage>("ErrorMessage");
        }

        public async Task DeleteErrorMessage(string id)
        {
            var filter = Builders<ErrorMessage>.Filter.Eq(em => em.id, new ObjectId(id));
            await _errorMsg.DeleteOneAsync(filter);
        }

        public async Task<List<ErrorMessage>> GetAllErrorMessage()
        {
            return await _errorMsg.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<ErrorMessage> GetErrorMessageByValue(int value)
        {
            return await _errorMsg.FindAsync(
                new BsonDocument { { "Value", value } }).Result.FirstOrDefaultAsync();
        }

        public async Task<ErrorMessage> GetErrorMessageById(string id)
        {
            return await _errorMsg.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<ErrorMessage> GetErrorMessageByCode(string code)
        {
            return await _errorMsg.FindAsync(
                new BsonDocument { { "code", code } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertErrorMessage(ErrorMessage errorMessage)
        {
            await _errorMsg.InsertOneAsync(errorMessage);
        }

        public async Task UpdateErrorMessage(ErrorMessage errorMessage)
        {
            FilterDefinition<ErrorMessage> filter = Builders<ErrorMessage>.Filter.Eq(em => em.id, errorMessage.id);
            await _errorMsg.UpdateOneAsync(filter, new ObjectUpdateDefinition<ErrorMessage>(errorMessage));
        }
    }
}