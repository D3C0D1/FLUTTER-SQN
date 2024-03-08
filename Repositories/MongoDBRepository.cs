using MongoDB.Driver;

namespace SQNBack.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase database;

        public MongoDBRepository()
        {
            client = new MongoClient("mongodb://127.0.0.1:27017");
            database = client.GetDatabase("SQNData");
        }
    }
}
