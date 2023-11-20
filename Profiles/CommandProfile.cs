using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

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

            

        }
        
    }
}