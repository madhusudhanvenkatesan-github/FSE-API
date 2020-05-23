using AutoMapper;
using FSE.API.DomainModel;
using FSE.API.Messages;

using System.Diagnostics.CodeAnalysis;


namespace FSE.API.DomainService.MapProfile
{
    [ExcludeFromCodeCoverage]
    public class UserMapProfile: Profile
    {
        public UserMapProfile()
        {

            CreateMap<PMUser, UserAddMsg>()
                .ForMember(dest => dest.EmployeeId, options =>
                  options.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.FirstName, options =>
                  options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options =>
                  options.MapFrom(dest => dest.LastName))
                .ReverseMap();

            CreateMap<PMUser, UserModMsg>()
               .ForMember(dest => dest.EmployeeId, options =>
                 options.MapFrom(src => src.EmployeeId))
               .ForMember(dest => dest.FirstName, options =>
                 options.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, options =>
                 options.MapFrom(dest => dest.LastName))
               .ForMember(dest=>dest.Id, options=>
               options.MapFrom(dest=>dest.Id))
               .ReverseMap();
        }

    }
}
