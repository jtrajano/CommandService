using System.Net.Http.Headers;
using AutoMapper;
using CommandService.Dtos;
using CommandService.Interface;
using CommandService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform {platformId}");

            if(!_repo.PlatformExist(platformId))
                return NotFound();
            
            var commands = _repo.GetCommandsForPlatfrom(platformId);
            var commandReadDto = _mapper.Map<IEnumerable<CommandReadDto>>(commands);

            return Ok(commandReadDto);
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId) {
            Console.WriteLine($"--> Hit GetCommandForPlatform {platformId}/ {commandId}");

            if (!_repo.PlatformExist(platformId))
                return NotFound();

            var command = _repo.GetCommand(platformId, commandId); 
            if (command == null)
                return NotFound();


            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return Ok(commandReadDto);
        }


        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
             Console.WriteLine($"--> Hit CreateCommandForPlatform {platformId}");

            if(!_repo.PlatformExist(platformId))
                return NotFound();


            var command = _mapper.Map<Command>(commandDto);

            _repo.CreateCommand(platformId, command);

            _repo.SaveChanges();


            var commandReadDto = _mapper.Map<CommandReadDto>(command);
   



            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId,commandId = command.Id }, commandReadDto);

        }

        



    }
}
