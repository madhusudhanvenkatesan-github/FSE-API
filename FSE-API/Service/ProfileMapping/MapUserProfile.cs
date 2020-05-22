using AutoMapper;
using FSE_API.Model;
using FSE_API.DBMessages;
using System.Diagnostics.CodeAnalysis;

namespace FSE_API.Service.ProfileMapping
{
    [ExcludeFromCodeCoverage]
    public class MapUserProfile : Profile
    {
        public MapUserProfile()
        {
            CreateMap<PMUser, AddUserMsg>()
                .ForMember(dest => dest.EmployeeId, options =>
                  options.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.FirstName, options =>
                  options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options =>
                  options.MapFrom(dest => dest.LastName))
                .ReverseMap();

            CreateMap<PMUser, ModelUserMsg>()
               .ForMember(dest => dest.EmployeeId, options =>
                 options.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FirstName, options =>
                 options.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, options =>
                 options.MapFrom(dest => dest.LastName))
               .ForMember(dest => dest.Id, options =>
                 options.MapFrom(dest => dest.Id))
               .ReverseMap();
        }
    }
}
