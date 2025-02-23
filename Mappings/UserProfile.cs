using ApiGodoy.Entities.User.UserDto;
using ApiGodoy.User;
using AutoMapper;

namespace ApiGodoyCordoba.Application.Mappings
{
    public class UserDataProfile : Profile
    {
        public UserDataProfile()
        {
            CreateMap<UserModel, CreateUserDto>().ReverseMap();
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, ResultUserDto>().ReverseMap();
            CreateMap<UpdateUserDto, UserModel>().ReverseMap();

        }

    }
}
