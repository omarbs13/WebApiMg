using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MdbApi.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace MdbApi.Data
{
    public class ApplicationContext
    {
        public IMongoDatabase _database { get; set; }
        public ApplicationContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }
    }
}
