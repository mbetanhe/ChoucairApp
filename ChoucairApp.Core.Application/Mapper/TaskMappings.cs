using AutoMapper;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Domain.Entities;

namespace ChoucairApp.Core.Application.Mapper
{
    public class TaskMappings : Profile
    {
        public TaskMappings()
        {
            CreateMap<TaskEntity, TaskDTO>()
                .ForMember(src => src.Title, opt => opt.MapFrom(dest => dest.Task_Title))
                .ForMember(src => src.Descripcion, opt => opt.MapFrom(dest => dest.Task_Description))
                .ForMember(src => src.ID, opt => opt.MapFrom(dest => dest.ID))
                .ForMember(src => src.EndDate, opt => opt.MapFrom(dest => dest.Task_EndDate))
                .ForMember(src => src.StatusId, opt => opt.MapFrom(dest => dest.StatusID))
                .ReverseMap();
        }
    }
}
