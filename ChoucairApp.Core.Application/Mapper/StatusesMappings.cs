using AutoMapper;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Domain.Entities;

namespace ChoucairApp.Core.Application.Mapper
{
    public class StatusesMappings : Profile
    {
        public StatusesMappings()
        {
            CreateMap<StatusEntity, StatusesDTO>()
            .ForMember(src => src.ID, opt => opt.MapFrom(dest => dest.ID))
            .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Status_Description))
            .ReverseMap();
        }
    }
}
