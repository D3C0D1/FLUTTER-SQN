using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class ServicesCollection : IServicesCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Service> _services;

        public ServicesCollection()
        {
            _services = _repository.database.GetCollection<Service>("Services");
        }

        public async Task DeleteService(string id)
        {
            var filter = Builders<Service>.Filter.Eq(s => s.id, new ObjectId(id));
            await _services.DeleteOneAsync(filter);
        }

        public async Task<List<Service>> GetAllServices()
        {
            return await _services.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Service> GetServiceById(string id)
        {
            return await _services.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task<List<Service>> GetServiceByCustomerAndStatus(string customer, string status)
        {
            var filter = Builders<Service>.Filter.And(Builders<Service>.Filter.Eq("CustomerId", customer),
                                                           Builders<Service>.Filter.Eq("Status", status));
            return await _services.FindAsync(filter).Result.ToListAsync();
        }

        public async Task<List<Service>> GetServiceByExpertAndStatus(string expert, string status)
        {
            var filter = Builders<Service>.Filter.And(Builders<Service>.Filter.Eq("ExpertId", expert),
                                                           Builders<Service>.Filter.Eq("Status", status));
            return await _services.FindAsync(filter).Result.ToListAsync();
        }

        public async Task<List<Service>> GetServicesByCustomer(string customer)
        {
            return await _services.FindAsync(new BsonDocument {
                { "Custome", customer } }).Result.ToListAsync();
        }

        public async Task<List<Service>> GetServicesByExpert(string expert)
        {
            return await _services.FindAsync(new BsonDocument {
                { "Custome", expert } }).Result.ToListAsync();
        }

        public async Task InsertService(Service service)
        {
            await _services.InsertOneAsync(service);
        }

        public async Task UpdateService(Service service)
        {
            FilterDefinition<Service> filter = Builders<Service>.Filter.Eq(s => s.id, service.id);
            await _services.UpdateOneAsync(filter, new ObjectUpdateDefinition<Service>(service));
        }
    }
}