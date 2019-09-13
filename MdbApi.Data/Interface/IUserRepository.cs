
using System.Collections.Generic;
using System.Threading.Tasks;
using MdbApi.Domain.Entities;

namespace MdbApi.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(string id);
        Task<User> Add(User item);
        Task<bool> Remove(string id);
        Task<bool> Update(string id, string body);
        Task<bool> UpdateDocument(User item);
    }
}