using ApiGodoy.Models;
using ApiGodoy.Models.Dto;
using AutoMapper;

namespace ApiGodoy.ApiMapper
{
    public class UserDataProfile : Profile
    {
        public UserDataProfile()
        {
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }

    }
}

