
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using AutoMapper;
using FSE.API.DomainModel;
using FSE.API.Messages;

namespace FSE.API.DomainService.MapProfile
{
    [ExcludeFromCodeCoverage]
    public class ProjectMapProfile:Profile
    {
        public ProjectMapProfile()
        {
            CreateMap<Project, ProjectAdd>()
                .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.End))
                .ForMember(dest => dest.PMUsrId, options => options.MapFrom(src => src.PMId))
                .ForMember(dest => dest.ProjectTitle, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Start))
                .ForMember(dest=>dest.Priority, options=>options.MapFrom(src=>src.Priority))
                .ReverseMap();

            CreateMap<Project, ProjectMod>()
                .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.End))
                .ForMember(dest => dest.PMUsrId, options => options.MapFrom(src => src.PMId))
                .ForMember(dest => dest.ProjectTitle, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Start))
                .ForMember(dest => dest.ProjId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Priority, options => options.MapFrom(src => src.Priority))
                .ReverseMap();

            CreateMap<ProjectUserVO, ProjectListing>()
                .ForMember(dest => dest.CompletedTaskCount, options =>
                  options.MapFrom(src => src.Projects.ProjectTasks.Count(tsk => tsk.Status == -1)))
                 .ForMember(dest => dest.EndDate, options =>
                  options.MapFrom(src => src.Projects.End))
                .ForMember(dest => dest.PMUsrId, options => options.MapFrom(src => src.Projects.PMId))
                .ForMember(dest => dest.ProjectTitle,
                options => options.MapFrom(src => src.Projects.Title))
                .ForMember(dest => dest.StartDate, options => options.MapFrom(src => src.Projects.Start))
                .ForMember(dest => dest.ProjId, options => options.MapFrom(src => src.Projects.Id))
                .ForMember(dest => dest.PMUsrName, options => options.MapFrom(src => src.UserName))
                .ForMember(dest => dest.TotalTaskCount,
                options => options.MapFrom(src => src.Projects.MaxTaskCount))
                .ForMember(dest => dest.Priority, options =>
                options.MapFrom(src => src.Projects.Priority));



        }
    }
}
