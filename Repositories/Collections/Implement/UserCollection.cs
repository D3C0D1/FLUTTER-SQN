using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class UserCollection : IUserCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<User> _users;

        public UserCollection()
        {
            _users = _repository.database.GetCollection<User>("User");
        }

        public async Task DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, new ObjectId(id));
            await _users.DeleteOneAsync(filter);
        }

        public async Task<User> GetUserById(string id)
        {
            return await _users.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByPerson(string person)
        {
            return await _users.FindAsync(
                new BsonDocument { { "Person", person } }).Result.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByPersonAndRole(string person, string role)
        {
            return await _users.FindAsync(
                new BsonDocument { { "UserName", person } }).Result.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _users.FindAsync(
                new BsonDocument { { "UserName", username } }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            return await _users.FindAsync(
                new BsonDocument { { "Role", role } }).Result.ToListAsync();
        }

        public async Task InsertUser(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.id, user.id);
            await _users.UpdateOneAsync(filter, new ObjectUpdateDefinition<User>(user));
        }
    }
}