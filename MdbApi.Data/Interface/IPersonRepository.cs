using System.Collections.Generic;
using System.Threading.Tasks;
using MdbApi.Domain.Entities;

namespace MdbApi.Data.Interface
{
    public interface IPersonRepository
    {
          Task<IEnumerable<Person>> GetAll();
        Task<Person> Get(string id);
        Task<Person> Add(Person item);
        Task<bool> Remove(string id);
        Task<bool> Update(string id, string body);
        Task<bool> UpdateDocument(Person item);
    }
}