using ApiGodoy.Models;
using ApiGodoy.Models.Dto;
using AutoMapper;

namespace ApiGodoy.ApiMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }

    }
}

