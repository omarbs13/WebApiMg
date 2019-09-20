using System.Collections.Generic;
using System.Threading.Tasks;
using MdbApi.Application.Models;

namespace MdbApi.Application.Interface
{
    public interface IPersonApplication
    {
          Task<Response<IEnumerable<PersonModel>>> GetAll();
        Task<Response<PersonModel>> Get(string id);
        Task<Response<PersonModelAdd>> Add(PersonModelAdd item);
        Task<Response<bool>> Remove(string id);
        Task<Response<bool>> Update(string id, string body);
        Task<Response<bool>> UpdateDocument(PersonModel item);
    }
}