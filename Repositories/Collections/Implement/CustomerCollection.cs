using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class CustomerCollection : ICustomerCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Customer> _customers;

        public CustomerCollection()
        {
            _customers = _repository.database.GetCollection<Customer>("Customer");
        }

        public async Task DeleteCustomer(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.id, new ObjectId(id));
            await _customers.DeleteOneAsync(filter);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customers.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Customer> GetCustomerByCellPhone(string cellPhone)
        {
            return await _customers.FindAsync(
                new BsonDocument { { "CellPhone", cellPhone } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _customers.FindAsync(
                new BsonDocument { { "Email", email } }).Result.FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerById(string id)
        {
            return await _customers.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task InsertCustomer(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq(c => c.id, customer.id);
            await _customers.UpdateOneAsync(filter, new ObjectUpdateDefinition<Customer>(customer));
        }
    }
}