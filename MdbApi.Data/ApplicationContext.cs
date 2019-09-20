using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MdbApi.Domain.Entities;

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

         public IMongoCollection<Person> Persons
        {
            get
            {
                return _database.GetCollection<Person>("Person");
            }
        }
    }
}
