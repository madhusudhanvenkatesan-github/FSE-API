using AutoMapper;
using FSE_API.Model;
using FSE_API.DBMessages;
using System.Diagnostics.CodeAnalysis;

namespace FSE_API.Service.ProfileMapping
{
    [ExcludeFromCodeCoverage]
    public class MapTaskProfile : Profile
    {
        public MapTaskProfile()
        {
            CreateMap<TaskProject, AddTask>()
                .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.ParentTaskId, options => options.MapFrom(src => src.ParentTaskId))
                .ForMember(dest => dest.Priority, options => options.MapFrom(src => src.Priority))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Start))
                .ForMember(dest => dest.TaskDescription, options =>
                  options.MapFrom(src => src.Name))
                .ForMember(dest => dest.TaskOwnerId, options => options.MapFrom(src => src.TaskOwnerId))
                .ReverseMap();
            CreateMap<TaskProject, TaskModal>()
                .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.ParentTaskId, options => options.MapFrom(src => src.ParentTaskId))
                .ForMember(dest => dest.Priority, options => options.MapFrom(src => src.Priority))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Start))
                .ForMember(dest => dest.TaskDescription, options =>
                  options.MapFrom(src => src.Name))
                .ForMember(dest => dest.TaskOwnerId, options => options.MapFrom(src => src.TaskOwnerId))
                .ForMember(dest => dest.TaskId, options => options.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
