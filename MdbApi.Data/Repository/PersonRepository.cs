using System.Collections.Generic;
using System.Threading.Tasks;
using MdbApi.Data.Interface;
using MdbApi.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MdbApi.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
         private readonly ApplicationContext _context = null;

        public PersonRepository(IOptions<Settings> settings)
        {
            _context = new ApplicationContext(settings);
        }
        public async Task<Person> Add(Person item)
        {
             await _context.Persons.InsertOneAsync(item);
            return item;
        }

        public async Task<Person> Get(string id)
        {
           var filter = Builders<Person>.Filter.Eq("Id", id);

            return await _context.Persons
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.Find(_ => true).ToListAsync();
        }

        public async Task<bool> Remove(string id)
        {
           DeleteResult actionResult = await _context.Persons.DeleteOneAsync(
                     Builders<Person>.Filter.Eq("Id", id));

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public Task<bool> Update(string id, string body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateDocument(Person item)
        {
             ReplaceOneResult actionResult
                = await _context.Persons
                                .ReplaceOneAsync(n => n.Id.Equals(item.Id)
                                        , item
                                        , new UpdateOptions { IsUpsert = true });
            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }
    }
}