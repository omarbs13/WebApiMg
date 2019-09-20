using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;
using MdbApi.Data.Interface;
using MdbApi.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MdbApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new ApplicationContext(settings);
        }

        public async Task<User> Add(User item)
        {
            await _context.Users.InsertOneAsync(item);
            return item;
        }

        public async Task<User> Get(string id)
        {
            var filter = Builders<User>.Filter.Eq("Id", id);

            return await _context.Users
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<bool> Remove(string id)
        {
            DeleteResult actionResult = await _context.Users.DeleteOneAsync(
                     Builders<User>.Filter.Eq("Id", id));

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public async Task<bool> Update(string id, string body)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id, id);
            var update = Builders<User>.Update
                            .Set(s => s.RoleName, body);
            //   .CurrentDate(s => s.UpdatedOn);

            UpdateResult actionResult
          = await _context.Users.UpdateOneAsync(filter, update);

            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateDocument(User item)
        {
            ReplaceOneResult actionResult
                = await _context.Users
                                .ReplaceOneAsync(n => n.Id.Equals(item.Id)
                                        , item
                                        , new UpdateOptions { IsUpsert = true });
            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }
    }
}