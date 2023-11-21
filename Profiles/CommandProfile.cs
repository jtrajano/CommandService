using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using PlatformService;

namespace CommandService.Profiles
{
    public class CommandProfile : Profile
    {

        public CommandProfile()
        {
            
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformReadDto, Platform>();

            CreateMap<Command, CommandCreateDto>();
            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandReadDto, Command>();
            CreateMap<Command, CommandReadDto>();

            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalID , opt => opt.MapFrom(src=> src.Id));

            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src=> src.PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src=> src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());

        }
        
    }
}