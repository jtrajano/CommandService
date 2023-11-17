using AutoMapper;
using CommandService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        [HttpPost]
        public IActionResult TestInboundConnection() {

            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of Platform Controller");
        
        }


    }


}
