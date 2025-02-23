
using ApiGodoy.Entities.SessionHistory;
using ApiGodoy.Entities.SessionHistory.SessionHistoryDto;
using AutoMapper;

namespace ApiGodoyCordoba.Application.ApiMapper
{
    public class SessionHistoryProfile : Profile
    {
        public SessionHistoryProfile()
        {
            CreateMap<SessionHistoryModel, CreateSessionHistoryDto>().ReverseMap();
            CreateMap<SessionHistoryModel, SessionHistoryDto>().ReverseMap();
            CreateMap<SessionHistoryModel, ResultSessionHistoryDto>().ReverseMap();
        }
    

    }
}

