using AutoMapper;
using MdbApi.Application.Models;
using MdbApi.Domain.Entities;

namespace MdbApi.Service.Helpers
{
    public class MappingsProfile: Profile
    {
        public MappingsProfile()
        {
            // CreateMap<User, UserModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();
            // CreateMap<User, UserAddModel>().ReverseMap();
            // CreateMap<User, UserModelToken>().ReverseMap();
            // CreateMap<Rol, RolModel>().ReverseMap();
            // CreateMap<UserProfile, UserProfileModel>().ReverseMap();
        }
    }
}
