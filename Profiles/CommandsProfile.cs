using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //source -> destination
            CreateMap<command, CommandReadDto>();
            CreateMap<CommandCreateDto, command>();
            CreateMap<CommandUpdateDto, command>();
            CreateMap<command, CommandUpdateDto>();
        }

    }
}