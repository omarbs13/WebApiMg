using System.Collections.Generic;
using System.Threading.Tasks;
using MdbApi.Application.Models;

namespace MdbApi.Application.Interface
{
    public interface IUserApplication
    {
         Task<Response<IEnumerable<UserModel>>> GetAll();
        Task<Response<UserModel>> Get(string id);
        Task<Response<UserModelAdd>> Add(UserModelAdd item);
        Task<Response<bool>> Remove(string id);
        Task<Response<bool>> Update(string id, string body);
        Task<Response<bool>> UpdateDocument(UserModel item);
    }
}