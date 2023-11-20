using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Interface;
using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;
        public CommandRepo(AppDbContext context)
        {
            _context = context;
            
        }

        public void CreateCommand(int platformId, Command command)
        {
            if(command == null)
                throw new ArgumentNullException(nameof(command));



            command.PlatformId= platformId;
            _context.Commands.Add(command);

            
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat == null)
                throw new ArgumentNullException(nameof(plat));
                
            _context.Platforms.Add(plat);
        }

        public bool ExternalPlatformExist(int externalPlatformId)
        {
            return _context.Platforms.Any(p => p.ExternalID == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
            
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(p=>p.PlatformId == platformId && p.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatfrom(int platformId)
        {
            return _context.Commands.Where(p=>p.PlatformId == platformId).ToList();
        }

        public bool PlatformExist(int platformId)
        {
            return _context.Platforms.Any(p=>p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges()>=0;
        }
    }
}