using AutoMapper;
using FSE.API.DomainModel;
using FSE.API.Messages;
using System.Diagnostics.CodeAnalysis;

namespace FSE.API.DomainService.MapProfile
{
    [ExcludeFromCodeCoverage]
    public class TaskMapProfile:Profile
    {
        public TaskMapProfile()
        {
            CreateMap<ProjTask, TaskAdd>()
                .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.ParentTaskId, options => options.MapFrom(src => src.ParentTaskId))
                .ForMember(dest => dest.Priority, options => options.MapFrom(src => src.Priority))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Start))
                .ForMember(dest => dest.TaskDescription, options =>
                  options.MapFrom(src => src.Name))
                .ForMember(dest => dest.TaskOwnerId, options => options.MapFrom(src => src.TaskOwnerId))
                .ReverseMap();
            CreateMap<ProjTask, TaskMod>()
                .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.ParentTaskId, options => options.MapFrom(src => src.ParentTaskId))
                .ForMember(dest => dest.Priority, options => options.MapFrom(src => src.Priority))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Start))
                .ForMember(dest => dest.TaskDescription, options =>
                  options.MapFrom(src => src.Name))
                .ForMember(dest => dest.TaskOwnerId, options => options.MapFrom(src => src.TaskOwnerId))
                .ForMember(dest=>dest.TaskId, options=>options.MapFrom(src=>src.Id))
                .ReverseMap();
        }
    }
}
