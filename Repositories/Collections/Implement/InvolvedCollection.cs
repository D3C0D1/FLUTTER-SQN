using MongoDB.Bson;
using MongoDB.Driver;
using SQNBack.Models;

namespace SQNBack.Repositories.Collections.Implement
{
    public class InvolvedCollection : IInvolvedCollection
    {
        internal readonly MongoDBRepository _repository = new();
        private readonly IMongoCollection<Involved> _formsInvolved;


        public InvolvedCollection()
        {
            _formsInvolved = _repository.database.GetCollection<Involved>("Involved");
        }
        public async Task<Involved> GetInvolvedById(string id)
        {
            return await _formsInvolved.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }

        public async Task DeleteInvolved(string id)
        {
            var filter = Builders<Involved>.Filter.Eq(e => e.id, new ObjectId(id));
            await _formsInvolved.DeleteOneAsync(filter);
        }

        public async Task<List<Involved>> GetAllInvolvedByReport(string report)
        {
            return await _formsInvolved.FindAsync(
            new BsonDocument { { "Report", report } }).Result.ToListAsync();

        }

        public async Task<List<Involved>> GetAllInvolvedByPlateNumber(string plateNumber)
        {
            return await _formsInvolved.FindAsync(
            new BsonDocument { { "PlateNumber", plateNumber } }).Result.ToListAsync();

        }



        public async Task InsertInvolved(Involved formInvolved)
        {
            await _formsInvolved.InsertOneAsync(formInvolved);
        }

        public async Task UpdateInvolved(Involved formInvolved)
        {
            FilterDefinition<Involved> filter = Builders<Involved>.Filter.Eq(em => em.id, formInvolved.id);
            await _formsInvolved.UpdateOneAsync(filter, new ObjectUpdateDefinition<Involved>(formInvolved));
        }


    }
}