using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class RoleCollection : IRoleCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Role> _roles;

        public RoleCollection()
        {
            _roles = _repository.database.GetCollection<Role>("Role");
        }
        public async Task DeleteRole(string id)
        {
            var filter = Builders<Role>.Filter.Eq(r => r.id, new ObjectId(id));
            await _roles.DeleteOneAsync(filter);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _roles.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _roles.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _roles.FindAsync(
                new BsonDocument { { "Name", name } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertRole(Role role)
        {
            await _roles.InsertOneAsync(role);
        }

        public async Task UpdateRole(Role role)
        {
            FilterDefinition<Role> filter = Builders<Role>.Filter.Eq(r => r.id, role.id);
            await _roles.UpdateOneAsync(filter, new ObjectUpdateDefinition<Role>(role));
        }
    }
}