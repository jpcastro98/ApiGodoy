using ApiGodoy.Entities.User.UserDto;
using ApiGodoy.Entities.UserData;
using ApiGodoy.Entities.UserData.UserDataDto;
using AutoMapper;

namespace ApiGodoyCordoba.Application.ApiMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDataModel, CreateUserDataDto>().ReverseMap();
            CreateMap<UserDataModel, UserDataDto>().ReverseMap();
            CreateMap<UpdateUserDataDto, UserDataModel>();
            CreateMap<UserDataModel, ResultUserDataDto >().ReverseMap();
        }

    }
}

